LANMachines
------------

Library to find all machines on your local network.
Machines have to be pingable..

Asynchronous pinging is carried out to get a list of machines quickly.

Usage

```c#
using (LanPingerAsync asyncPinger = new LanPingerAsync())
{
    List<string> activeAddresses = asyncPinger.GetActiveMachines();
} // end using
```

Source:
http://stackoverflow.com/questions/4042789/how-to-get-ip-of-all-hosts-in-lan