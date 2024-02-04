@tool

extends MultiMusicPlayer
class_name MusicSwitch

enum Transition {CROSSFADE, FADE_IN_OUT, CUT}

@export var transition: Transition
@export var transition_time: float
var audio_ids = {}
var currently_playing: GoConductorNode

func play():
	super.play()
	if currently_playing != null: currently_playing.play()

func pause():
	super.pause()
	if currently_playing != null: currently_playing.pause()

func stop():
	super.stop()
	if currently_playing != null: currently_playing.stop()

func cue(track_name: String):
	# Find the new track by name
	var new_track = find_track(track_name)
	
	if new_track == null or new_track == currently_playing:
		return false
	
	# If we're not already playing a track we don't need to do anything	
	# Otherwise, do the apropriate transition
	if currently_playing != null:
		match transition:
			Transition.CROSSFADE: 
				crossfade(new_track)
			Transition.FADE_IN_OUT: 
				fade_in_out(new_track)
			Transition.CUT, _: 
				cut(new_track)
	
	# Set currently playing to the new track
	currently_playing = new_track
	print("changed currently playing")
	return true

func crossfade(new_track):
	# Save the currently playing track as a variable as it will be reassigned over the course of the tween
	var prev_track = currently_playing
	# Create two new busses
	# Bus a for old music fading out, at position i
	# Bus b for new music fading in, at position j 
	AudioServer.add_bus()
	var i = AudioServer.bus_count-1
	AudioServer.add_bus()
	var j = i + 1
	var original_bus = prev_track.get_bus()
	var bus_a = AudioServer.get_bus_name(i)
	var bus_b = AudioServer.get_bus_name(j)
	# Send new busses to the original_bus
	AudioServer.set_bus_send(i, original_bus)
	AudioServer.set_bus_send(j, original_bus)
	
	# Add amps to new busses, to be used for fading
	# Amp B will start at -30db
	var amp_a = AudioEffectAmplify.new()
	var amp_b = AudioEffectAmplify.new()
	amp_b.set_volume_db(-30)
	AudioServer.add_bus_effect(i, amp_a)
	AudioServer.add_bus_effect(j, amp_b)
	
	# Direct tracks to their respective busses
	# and start track B
	prev_track.set_bus(bus_a)
	new_track.set_bus(bus_b)
	new_track.play()
	
	# Set up the tween
	var fade_tween = create_tween()
	fade_tween.tween_property(amp_a, "volume_db", -30, transition_time)
	fade_tween.parallel().tween_property(amp_b, "volume_db", 0, transition_time)
	
	# Add the tween callbacks (what happens after they stop)
	# Stop track a, remove the temp busses 
	var cleanup = func ():
		prev_track.stop()
		AudioServer.set_bus_mute(i, true)
		AudioServer.remove_bus(i)
		AudioServer.remove_bus(j-1)
	fade_tween.tween_callback(cleanup)
	print("transition cleanup done")

func fade_in_out(new_track):
	pass

func cut(new_track):
	pass

func _ready():
	update_tracks()
		
