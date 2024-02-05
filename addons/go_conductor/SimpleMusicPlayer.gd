@tool

class_name SimpleMusicPlayer
extends GoConductorNode

var audio_player
@export var loop = true

func _ready():
	audio_player = get_child(0)
	audio_player.finished.connect(on_audio_player_finished)

# TODO I don't like this they don't impliment the super rule
func play():
	if playing:
		return
	playing = true
	audio_player.play(play_from_position)

func play_from(position: int):
	playing = true
	play_from_position = position
	audio_player.play()

func pause():
	play_from_position = audio_player.get_playback_position()
	playing = false
	audio_player.stop()
	
func stop():
	play_from_position = 0
	playing = false
	audio_player.stop()

func get_playback_position():
	var playback_position = audio_player.get_playback_position() 
	playback_position += AudioServer.get_time_to_next_mix()
	return playback_position

func get_bus():
	return audio_player.get_bus()

func set_bus(new_bus: String):
	audio_player.set_bus(new_bus)

func on_audio_player_finished():
	if loop:
		audio_player.play()
	track_end.emit()

func _get_configuration_warnings():
	var valid_child_warning = ["First child must be AudioStreamPlayer(2d/3d)"]
	
	if get_child_count() < 1:
		return valid_child_warning
	if !is_audio_stream_player(get_child(0)):
		return valid_child_warning
