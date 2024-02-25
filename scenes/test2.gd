extends Node2D

@onready var music = $switch


func _ready():
	music.Cue("northstar")


func _process(delta):
	if Input.is_action_just_pressed("numpad_1"):
		music.Play()
	elif Input.is_action_just_pressed("numpad_2"):
		music.Pause()
	elif Input.is_action_just_pressed("numpad_3"): music.Stop()
		
