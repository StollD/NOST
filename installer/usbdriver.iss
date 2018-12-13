[CustomMessages]
usbdriver_title=Nokia USB Driver 
usbdriver_size=26.7 MB

[Code]
const
	usbdriver_url = 'http://github.com/StollD/nokia-driver-installer/releases/download/v1.1.0/Phone_Nokia_USB_Driver_v1.1.0.exe';  
	usbdriver_upgradecode = '{5DEB6D92-C39B-4DC3-8885-0474F6DEFC9A}';

procedure usbdriver(minVersion: string);
begin
	if (not IsIA64()) then begin
		if (not msiproductupgrade(GetString(usbdriver_upgradecode, '', ''), minVersion)) then
			AddProduct('usbdriver' + GetArchitectureString() + '.exe',
				'/VERYSILENT /SUPPRESSMSGBOXES /NORESTART',
				CustomMessage('usbdriver_title' + GetArchitectureString()),
				CustomMessage('usbdriver_size' + GetArchitectureString()),
				GetString(usbdriver_url, '', ''),
				false, false, false);
	end;
end;

[Setup]
