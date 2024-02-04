extends Node2D


@onready var music = $music


# Called when the node enters the scene tree for the first time.
func _ready():
	music.cue("drums")
	music.play()


func _on_pause_pressed():
	music.pause()


func _on_play_pressed():
	music.play()


func _on_stop_pressed():
	music.stop()


func _on_restart_pressed():
	music.restart()


func _on_change_pressed():
	music.cue("bass")


func _on_fade_to_drums_pressed():
	music.cue("drums")


func _on_fade_to_multi_pressed():
	music.cue("multi")
