extends Node2D

@onready var music_conductor = $MusicConductor

func _ready():
	music_conductor.play()

func _on_button_a_toggled(button_pressed):
	music_conductor.cue_track("a", button_pressed)

func _on_button_b_toggled(button_pressed):
	music_conductor.cue_track("b", button_pressed)
