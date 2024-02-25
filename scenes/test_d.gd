extends "res://scenes/test_universal.gd"

func _ready():
	music.Cue("ebm")

func _on_track_switch_toggled(button_pressed):
	if button_pressed:
		music.Cue("ebm")
	else:
		music.Cue("northstar")
