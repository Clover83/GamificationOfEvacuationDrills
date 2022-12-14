# GamificationOfEvacuationDrills


## Instructions for setting up the project

### Mobile app
1. Download Unity version 2021.3.14f1 (other versions may work but use at your own discretion). You can download it with Unity Hub and [the Unity download archive](https://unity.com/releases/editor/archive).
2. Clone this repository.
3. Open the repository folder in Unity.
4. If you want access to the server data, you need to create a creds.json file (as described below).
5. Build the project.

### Creds.json

API keys are sensitive information and should not be stored in a public GitHub repository. 
Because of this, the API keys are stored in `Assets/Resources/creds.json` and this file will need to be created if you want to send the users' position data to JSONBin.
If you are extending the app to instead send the data to another server (such as GEDServer), it will be wise to store similarly sensitive information using this system as well.
For more in depth implementation instructions, see the APIKeys class.

#### Creating the file
The variables in creds.json should follow the naming fo the fields in the APIKeys class. The most up to date version should look like this:
```json
{
	"googleMaps":"[REPLACE WITH YOUR KEY]",
	"jsonBinMaster":"[REPLACE WITH YOUR KEY]",
	"jsonBinAccess":"[REPLACE WITH YOUR KEY]"
}
```
(Replace the "[REPLACE WITH YOUR KEY]" with your key from the corresponding service)


### Webserver
See [the webserver's repository](https://github.com/Clover83/GEDServer) for setup instructions.
