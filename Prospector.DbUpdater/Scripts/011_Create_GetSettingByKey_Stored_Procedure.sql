Drop Procedure If Exists spGetSettingByKey;

Delimiter $$

Create Procedure spGetSettingByKey
(
	SettingKey NVarChar(50)
)
Begin

	Select
		Settings.SettingsKey As SettingsKey,
		Settings.SettingsValue As SettingsValue
	From
		Settings
	Where
		Settings.SettingsKey = SettingKey;

End$$

Delimiter;