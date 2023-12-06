﻿using System.Collections.Generic;
using System.Linq;
using Pulumi;
using AzureNative = Pulumi.AzureNative;

return await Deployment.RunAsync(() => 
{
    var azureFirewall = new AzureNative.Network.AzureFirewall("azureFirewall", new()
    {
        ApplicationRuleCollections = new[]
        {
            new AzureNative.Network.Inputs.AzureFirewallApplicationRuleCollectionArgs
            {
                Action = new AzureNative.Network.Inputs.AzureFirewallRCActionArgs
                {
                    Type = "Deny",
                },
                Name = "apprulecoll",
                Priority = 110,
                Rules = new[]
                {
                    new AzureNative.Network.Inputs.AzureFirewallApplicationRuleArgs
                    {
                        Description = "Deny inbound rule",
                        Name = "rule1",
                        Protocols = new[]
                        {
                            new AzureNative.Network.Inputs.AzureFirewallApplicationRuleProtocolArgs
                            {
                                Port = 443,
                                ProtocolType = "Https",
                            },
                        },
                        SourceAddresses = new[]
                        {
                            "216.58.216.164",
                            "10.0.0.0/24",
                        },
                        TargetFqdns = new[]
                        {
                            "www.test.com",
                        },
                    },
                },
            },
        },
        AzureFirewallName = "azurefirewall",
        IpConfigurations = new[]
        {
            new AzureNative.Network.Inputs.AzureFirewallIPConfigurationArgs
            {
                Name = "azureFirewallIpConfiguration",
                PublicIPAddress = new AzureNative.Network.Inputs.SubResourceArgs
                {
                    Id = "/subscriptions/subid/resourceGroups/rg1/providers/Microsoft.Network/publicIPAddresses/pipName",
                },
                Subnet = new AzureNative.Network.Inputs.SubResourceArgs
                {
                    Id = "/subscriptions/subid/resourceGroups/rg1/providers/Microsoft.Network/virtualNetworks/vnet2/subnets/AzureFirewallSubnet",
                },
            },
        },
        Location = "West US",
        NatRuleCollections = new[]
        {
            new AzureNative.Network.Inputs.AzureFirewallNatRuleCollectionArgs
            {
                Action = new AzureNative.Network.Inputs.AzureFirewallNatRCActionArgs
                {
                    Type = "Dnat",
                },
                Name = "natrulecoll",
                Priority = 112,
                Rules = new[]
                {
                    new AzureNative.Network.Inputs.AzureFirewallNatRuleArgs
                    {
                        Description = "D-NAT all outbound web traffic for inspection",
                        DestinationAddresses = new[]
                        {
                            "1.2.3.4",
                        },
                        DestinationPorts = new[]
                        {
                            "443",
                        },
                        Name = "DNAT-HTTPS-traffic",
                        Protocols = new[]
                        {
                            "TCP",
                        },
                        SourceAddresses = new[]
                        {
                            "*",
                        },
                        TranslatedAddress = "1.2.3.5",
                        TranslatedPort = "8443",
                    },
                    new AzureNative.Network.Inputs.AzureFirewallNatRuleArgs
                    {
                        Description = "D-NAT all inbound web traffic for inspection",
                        DestinationAddresses = new[]
                        {
                            "1.2.3.4",
                        },
                        DestinationPorts = new[]
                        {
                            "80",
                        },
                        Name = "DNAT-HTTP-traffic-With-FQDN",
                        Protocols = new[]
                        {
                            "TCP",
                        },
                        SourceAddresses = new[]
                        {
                            "*",
                        },
                        TranslatedFqdn = "internalhttpserver",
                        TranslatedPort = "880",
                    },
                },
            },
        },
        NetworkRuleCollections = new[]
        {
            new AzureNative.Network.Inputs.AzureFirewallNetworkRuleCollectionArgs
            {
                Action = new AzureNative.Network.Inputs.AzureFirewallRCActionArgs
                {
                    Type = "Deny",
                },
                Name = "netrulecoll",
                Priority = 112,
                Rules = new[]
                {
                    new AzureNative.Network.Inputs.AzureFirewallNetworkRuleArgs
                    {
                        Description = "Block traffic based on source IPs and ports",
                        DestinationAddresses = new[]
                        {
                            "*",
                        },
                        DestinationPorts = new[]
                        {
                            "443-444",
                            "8443",
                        },
                        Name = "L4-traffic",
                        Protocols = new[]
                        {
                            "TCP",
                        },
                        SourceAddresses = new[]
                        {
                            "192.168.1.1-192.168.1.12",
                            "10.1.4.12-10.1.4.255",
                        },
                    },
                    new AzureNative.Network.Inputs.AzureFirewallNetworkRuleArgs
                    {
                        Description = "Block traffic based on source IPs and ports to amazon",
                        DestinationFqdns = new[]
                        {
                            "www.amazon.com",
                        },
                        DestinationPorts = new[]
                        {
                            "443-444",
                            "8443",
                        },
                        Name = "L4-traffic-with-FQDN",
                        Protocols = new[]
                        {
                            "TCP",
                        },
                        SourceAddresses = new[]
                        {
                            "10.2.4.12-10.2.4.255",
                        },
                    },
                },
            },
        },
        ResourceGroupName = "rg1",
        Sku = new AzureNative.Network.Inputs.AzureFirewallSkuArgs
        {
            Name = "AZFW_VNet",
            Tier = "Standard",
        },
        Tags = 
        {
            { "key1", "value1" },
        },
        ThreatIntelMode = "Alert",
        Zones = new[] {},
    });

});
