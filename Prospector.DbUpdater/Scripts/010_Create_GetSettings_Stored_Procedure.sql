Drop Procedure If Exists spGetSettings;

Delimiter $$

Create Procedure spGetSettings
(
)
Begin

	Select
		Settings.SettingsKey As SettingsKey,
		Settings.SettingsValue As SettingsValue
	From
		Settings;

End$$

Delimiter ;