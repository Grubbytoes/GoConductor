extends Node2D

@onready var music_conductor = $MusicConductor

func _ready():
	music_conductor.play()


func _on_new_pressed():
	music_conductor.cue_in("a")
