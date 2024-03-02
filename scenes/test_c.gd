extends "res://scenes/test_universal.gd"

@onready var ebm = $music/ebm

func _ready():
	music.Cue("ebm")
	ebm.Cue("backing")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	super._process(delta)
	
	if Input.is_action_just_pressed("numpad_6"): ebm.Cue("backing")


func _on_track_switch_toggled(button_pressed):
	if button_pressed:
		music.Cue("northstar")
	else:
		music.Cue("ebm")
