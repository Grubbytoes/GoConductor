@tool

extends Node
class_name GoConductorNode

var play_from_position: float
var playing = false
signal track_end

func play():
	push_error("UNIMPLIMENTED PLAY METHOD")

func play_from(position: int):
	push_error("UNIMPLIMENTED PLAY FROM METHOD")

func pause():
	push_error("UNIMPLIMENTED METHOD")
	
func stop():
	push_error("UNIMPLIMENTED METHOD")

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
	push_error("UNIMPLIMENTED METHOD")
	return ""

func set_bus(bus_name: String):
	push_error("UNIMPLIMENTED METHOD")

func get_playback_position():
	push_error("UNIMPLIMENTED METHOD")

func get_play_from_position():
	return play_from_position

func set_play_from_position(from_position):
	play_from_position = from_position
