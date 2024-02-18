extends Node2D
@onready var music_switch = $musicSwitch
@onready var music_conductor = $musicSwitch/ebm


func _ready():
	music_switch.Cue("ebm")
	music_conductor.CueIn("drums")
	
func _process(delta):
	if Input.is_action_just_pressed("numpad_1"): music_switch.Play()
	elif Input.is_action_just_pressed("numpad_2"): music_switch.Pause()
	elif  Input.is_action_just_pressed("numpad_3"): music_switch.Stop()
	elif Input.is_action_just_pressed("numpad_4"): music_switch.PlayFrom(2.0)
	elif Input.is_action_just_pressed("numpad_5"): music_switch.Restart()
