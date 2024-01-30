@tool

extends GoConductorNode
class_name MultiMusicPlayer

var tracks = {}

# Ideally this should only have to be called once - on ready
# Updates the dictionary of tracks to be all valid children of the node
func update_tracks():
	for child in get_children():
		if is_music_player(child):
			tracks[child.name] = child

func _get_configuration_warnings():
	if tracks.is_empty():
		return ["No tracks to play"]
		
