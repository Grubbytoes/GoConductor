@tool
extends EditorPlugin

const go_conductor_icon = preload("ugly_go_conductor.png")

const go_conductor_node = preload("goConductorNode.gd")
const simple_music_player = preload("SimpleMusicPlayer.gd")
const multi_music_player = preload("MultiMusicPlayer.gd")
const music_switch = preload("MusicSwitch.gd")

func _enter_tree():
	add_custom_type("GoConductorNode", "Node", go_conductor_node, go_conductor_icon)
	add_custom_type("SimpleMusicPlayer", "GoConductorNode", simple_music_player, go_conductor_icon)
	add_custom_type("MusicSwitch", "GoConductorNode", music_switch, go_conductor_icon)


func _exit_tree():
	remove_custom_type("GoConductorNode")
	remove_custom_type("SimpleMusicPlayer")
	remove_custom_type("MusicSwitch")
