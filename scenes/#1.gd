extends Node2D
@onready var node = $Node

func _ready():
	node.Play()


func _process(delta):
	if Input.is_action_just_pressed("ui_accept"):
		node.TogglePause()
