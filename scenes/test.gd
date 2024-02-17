extends Node2D
@onready var music_switch = $musicSwitch
@onready var music_conductor = $musicSwitch/ebm


func _ready():
	music_switch.Cue("ebm")
	music_switch.Play()
	
func _process(delta):
	if Input.is_action_just_pressed("numpad_1"): music_switch.Play()
	elif Input.is_action_just_pressed("numpad_2"): music_switch.Pause()
