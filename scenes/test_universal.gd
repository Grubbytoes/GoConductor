extends Node2D
@onready var music = $music

var toggle = true;
	
func _process(delta):
	if Input.is_action_just_pressed("numpad_1"): music.Play()
	elif Input.is_action_just_pressed("numpad_2"): music.Pause()
	elif  Input.is_action_just_pressed("numpad_3"): music.Stop()
	elif Input.is_action_just_pressed("numpad_4"): music.PlayFrom(2.0)
	elif Input.is_action_just_pressed("numpad_5"): music.Restart()
