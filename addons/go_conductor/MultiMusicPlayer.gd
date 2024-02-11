@tool

extends GoConductorNode
class_name MultiMusicPlayer

# Marked as private to discorage direct useage
var _tracks = {}

# To be used where possible instead of directly referencing tracks
func find_track(track_name: String) -> GoConductorNode:
	var track = _tracks.get(track_name)
	
	if track == null:
		update_tracks()
		track = _tracks.get(track_name)
	
	return track

# Ideally this should only have to be called once - on ready
# Updates the dictionary of tracks to be all valid children of the node
func update_tracks():
	for child in get_children():
		print(child)
		if is_music_player(child):
			_tracks[child.name] = child

# A function to return an array of all tracks
func get_all_tracks():
	return _tracks.values()

func _ready():
	update_tracks()

func _get_configuration_warnings():
	if _tracks.is_empty():
		return ["No tracks to play"]
		
