extends "res://scenes/test_universal.gd"


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	super._process(delta)
	
	if Input.is_action_just_pressed("numpad_6"): music.Cue("backing")
