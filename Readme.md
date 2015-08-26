LANMachines
===========

Library to find all machines on your local network.
Machines have to be pingable.

Asynchronous pinging is carried out to get a list of machines quickly.

Usage
-----

```c#
using (LanPingerAsync asyncPinger = new LanPingerAsync())
{
    // all reachable IP addresses as a list of strings
    List<string> activeAddresses = asyncPinger.GetActiveMachines();
} // end using
```


[Based on this nice and useful stackoverflow post](http://stackoverflow.com/questions/4042789/how-to-get-ip-of-all-hosts-in-lan)

*Changes have been made, usage needs to be updated as well as description as pinging isn't all that's used anymore..