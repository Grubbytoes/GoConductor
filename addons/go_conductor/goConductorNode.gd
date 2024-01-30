@tool

extends Node
class_name GoConductorNode

var play_from_position: float
var playing = false
signal track_end

func play():
	pass

func pause():
	pass
	
func stop():
	pass

func restart():
	stop()
	play()

func is_audio_stream_player(node: Node) -> bool:
	# This is gross and I hate it
	if node is AudioStreamPlayer:
		return true
	elif node is AudioStreamPlayer2D:
		return true
	elif node is AudioStreamPlayer3D:
		return true
	return false

func is_music_player(node: Node) -> bool:
	return (is_audio_stream_player(node) or node is GoConductorNode)

func get_bus() -> String:
	return ""

func set_bus(bus_name: String):
	pass

func get_playback_position():
	pass

func get_play_from_position():
	return play_from_position

func set_play_from_position(from_position):
	play_from_position = from_position
