using System.Collections.Generic;
using Pulumi.Kubernetes.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;

return await Pulumi.Deployment.RunAsync(() =>
{
    var coreDnsDeploymentPatch = new DeploymentPatch("coredns", new DeploymentPatchArgs
    {
       Metadata = new ObjectMetaPatchArgs { Namespace = "kube-system", Name = "coredns" },
       Spec = new DeploymentSpecPatchArgs
       {
           Template = new PodTemplateSpecPatchArgs
           {
               Spec = new PodSpecPatchArgs
               {
                   Volumes =
                   {
                       new VolumePatchArgs
                       {
                           Name = "my-new-volume",
                           ConfigMap = new ConfigMapVolumeSourcePatchArgs
                           {
                               DefaultMode = 420,
                               Name = "some-config-map",
                               Items =
                               {
                                   new KeyToPathPatchArgs
                                   {
                                       Key = "key-1",
                                       Path = "path-1"
                                   }
                               }
                           }
                       }
                   },
               }
           }
       }
    });
});
