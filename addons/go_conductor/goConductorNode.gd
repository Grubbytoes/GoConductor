@tool

extends Node
class_name GoConductorNode

var currently_playing = null
var play_from_position: float
var playing = false

func play():
	currently_playing.play()

func pause():
	currently_playing.pause()
	
func stop():
	currently_playing.stop()

func restart():
	currently_playing.stop()
	currently_playing.play()

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
	return currently_playing.get_bus()

func set_bus(bus_name: String):
	currently_playing.set_bus(bus_name)

func get_playback_position():
	currently_playing.get_playback_position()

func get_play_from_position():
	return play_from_position

func set_play_from_position(from_position):
	play_from_position = from_position
