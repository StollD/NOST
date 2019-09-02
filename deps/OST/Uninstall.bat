@echo off

RD /S /Q "C:\Program Files (x86)\OST LA\PST\StandardForm2"
RD /S /Q "C:\Program Files (x86)\OST LA\PST\StandardForm3"
RD /S /Q "C:\Program Files (x86)\OST LA\PST\StandardForm4"
RD /S /Q "C:\Program Files (x86)\OST LA\PST\StandardForm5"
RD /S /Q "C:\Program Files (x86)\OST LA\PST\StandardForm6"
RD /S /Q "C:\Program Files (x86)\OST LA\PST\StandardForm7"
RD /S /Q "C:\Program Files (x86)\OST LA\PST\StandardForm8"

msiexec /x %1 /qr