extends "res://scenes/test_universal.gd"

@onready var ebm = $music/ebm

func _ready():
	music.Cue("northstar")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	super._process(delta)
	
	if Input.is_action_just_pressed("numpad_6"): ebm.Cue("backing")


func _on_option_button_item_selected(index):
	print("ping")
	match index:
		0:
			music.Cue("northstar")
		1:
			music.Cue("ebm")
		2:
			music.Cue("tangerine")
		_:
			music.Cue("northstar")
