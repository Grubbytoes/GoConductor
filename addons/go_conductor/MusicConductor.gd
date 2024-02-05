@tool

extends MultiMusicPlayer
class_name MusicConductor

var config_ok
var currently_playing: Array[GoConductorNode]
var lead: GoConductorNode
var t: float = 0.0
var attack = 0.5

func _ready():
	# Fill the dictionary with the current tracks
	update_tracks()
	lead = get_child(0)
	lead.track_end.connect(on_lead_track_end)

func play():
	super.play()
	
	for track in get_all_tracks():
		track.play()

func pause():
	super.pause()
	
	for track in get_all_tracks():
		track.pause()	

func stop():
	super.stop()
	
	for track in get_all_tracks():
		track.stop()	
	
func on_lead_track_end():
	print("track ended")
	
func cue_track(track_name: String, play_track = true) -> void:
	var track: GoConductorNode = find_track(track_name)
	track.mute(!play_track, attack)

func get_bus() -> String:
	return lead.get_bus()

func set_bus(bus_name: String):
	for track in get_all_tracks():
		track.set_bus(bus_name)

func _get_configuration_warnings():
	var warning = []
	if get_child_count() < 1:
		warning.append("Conductor has no children")
	elif !(get_child(0) is GoConductorNode):
		warning.append("First child must be a ")
	return warning
