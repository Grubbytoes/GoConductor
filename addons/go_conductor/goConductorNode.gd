@tool

extends Node
class_name GoConductorNode

var play_from_position: float
var playing = false
signal track_end

# The ONLY functionality that these methods have implimented at this level is handeling the 'playing' flag, as this is the only thing that all HoConductorNodes will have in common
# All others will act differently depending on what the class is for, but a call to super should be included in all to ensure that 'playing' behaves consistently. 
# Think of these as abstract methods. 
# PLAY
# PLAY FROM
# PAUSE
# STOP
func play():
	playing = true

func play_from(position: int):
	playing = true

func pause():
	playing = false
	
func stop():
	playing = false

# The restart function should be as simple as stoping and starting again instantly, in most sub classes it shouldn't need its own override,
# so long as the above four are implimented properly
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
