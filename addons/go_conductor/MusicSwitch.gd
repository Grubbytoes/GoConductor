@tool

extends MultiMusicPlayer
class_name MusicSwitch

enum Transition {CROSSFADE, FADE_IN_OUT, CUT}

@export var transition: Transition
@export var transition_time: float
var audio_ids = {}
var currently_playing: GoConductorNode
var _transition_in_progress = false

func play():
	super.play()
	if currently_playing != null: currently_playing.play()

func play_from(position: int):
	super.play_from(position)
	currently_playing.play_from(position)

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
	else:
		currently_playing = new_track

	return true

func crossfade(new_track):
	# First, DEFINITIVE variables for ingoing and outgoing tracks
	# Then update currently_playing
	var outgoing = currently_playing
	var incomming = new_track
	currently_playing = new_track
	_transition_in_progress = true

	# Set the new track to a lower volume, 30db lower than 'current', and play
	incomming.set_volume_db(incomming.volume_db_at_ready-30)
	incomming.play()

	# Instance our tweens
	var incomming_tween: Tween = create_tween()
	var outgoing_tween: Tween = create_tween()

	# Add tweeners and callback to incoming
	incomming_tween.tween_property(incomming.audio_player, "volume_db", 30, transition_time).as_relative()
	incomming_tween.tween_callback(func (): _transition_in_progress = false)

	# Add tweeners and callback to outgoing
	outgoing_tween.tween_property(outgoing.audio_player, "volume_db", -30, transition_time).as_relative()
	outgoing_tween.tween_callback(outgoing.stop)
	outgoing_tween.tween_callback(outgoing.reset_volume_db)

	# start both
	incomming_tween.play()
	outgoing_tween.play()



func fade_in_out(new_track):
	pass

func cut(new_track):
	pass
		
