@tool

extends MultiMusicPlayer
class_name MusicConductor

var config_ok
var currently_playing: Array[GoConductorNode]
var lead: GoConductorNode
var t: float = 0.0

func _ready():
	update_tracks()
	lead = get_child(0)
	lead.track_end.connect(on_lead_track_end)

func play():
	super.play()
	lead.play()

func on_lead_track_end():
	print("track ended")
	
func cue_in(track_name: String):
	var track = tracks.get(track_name)
	var position = t
	track.play_from(position)

func cue_out(track_name: String):
	var track = tracks.get(track_name)
	track.stop()

func _process(delta):
	if playing: t += delta

func _get_configuration_warnings():
	var warning = []
	if get_child_count() < 1:
		warning.append("Conductor has no children")
	elif !(get_child(0) is GoConductorNode):
		warning.append("First child must be a ")
	return warning
