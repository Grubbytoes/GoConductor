extends Node2D

@onready var music_conductor = $MusicConductor

func _ready():
	music_conductor.play()
