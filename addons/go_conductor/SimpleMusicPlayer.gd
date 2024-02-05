@tool

class_name SimpleMusicPlayer
extends GoConductorNode

var audio_player
var volume_db = 0.0 # TODO
@export var loop = true

func mute(muted = true, attack = 0.0):
	# Instant muting
	if attack <= 0.0:			
		if muted:
			audio_player.set_volume_db(-INF)
		else:
			audio_player.set_volume_db(volume_db)
	# Muting with fade
	else:
		if muted:
			tween_to_volume(-INF, attack)
		else:
			tween_to_volume(volume_db, attack)
	
# Sets volume_db via a tween
func tween_to_volume(final: float, duration: float):
	# The reason we do this instead of set_volume_db is because we're actually changing the volume itself later
	var tween: Tween = get_tree().create_tween()
	tween.tween_property(audio_player, "volume_db", final, duration)
	tween.play()


# TODO I don't like this they don't impliment the super rule
# Fix play, play_from, pause, stop
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

func set_volume_db(val: float):
	volume_db = val
	audio_player.set_volume_db(val)

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

func _ready():
	audio_player = get_child(0)
	audio_player.finished.connect(on_audio_player_finished)