extends Node

@onready var music = $"../music"


func _ready():
	music.CueIn("backing")

