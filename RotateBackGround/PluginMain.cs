using BveEx.PluginHost.Plugins;
using BveTypes.ClassWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using ObjectiveHarmonyPatch;
using SlimDX;
using BveEx.Extensions.MapStatements;

namespace BveEx.Toukaitetudou.RotateBackGround
{
    [Plugin(PluginType.MapPlugin)]
    public class PluginMain : AssemblyPluginBase
    {
        HarmonyPatch Patch { get; }
        IStatementSet Statements { get; }

        class Data
        {

            public double FromLocation { get; }
            public double ToLocation { get; }
            public float RotateRadPerSec { get; }

            public Data(double fromLocation,double toLocation,float rotateRadPerSec)
            {
                FromLocation = fromLocation;
                ToLocation = toLocation;
                RotateRadPerSec = rotateRadPerSec;

            }
        }
        List<Data> data;
        public PluginMain(PluginBuilder builder) : base(builder)
        {

            Patch= HarmonyPatch.Patch(nameof(Patch),BveHacker.BveTypes.GetClassInfoOf<Background>().GetSourceMethodOf(nameof(Background.Draw)).Source,PatchType.Prefix);
            Patch.Invoked+=Patch_Invoked;
            Statements=Extensions.GetExtension<IStatementSet>();

            Statements.LoadingCompleted+=Statements_LoadingCompleted;
        }

        private void Statements_LoadingCompleted(object sender, EventArgs e)
        {

            data = new List<Data>();
            {

            }
            List<(double location, double rotate)> statementData = Statements.FindUserStatements(nameof(Toukaitetudou), ClauseFilter.Element(nameof(RotateBackGround), 0), ClauseFilter.Function("Change", 1)).Select(x => (x.Source.Location, Convert.ToDouble(x.Source.Clauses[4].Args[0]))).OrderBy(x=>x.Location).ToList();
            for (int i=0;i<statementData.Count;++i)
            {
                (double location, double rotate) = statementData[i];
               
                data.Add(new Data(location, (i+1>=statementData.Count ? double.MaxValue : statementData[i+1].location), (float)rotate));
            }

        }

        private PatchInvokationResult Patch_Invoked(object sender, PatchInvokedEventArgs e)
        {
            Background background= Background.FromSource(e.Instance);
            Direct3DProvider direct3DProvider=Direct3DProvider.FromSource( e.Args[0]);
            Matrix matrix=(Matrix)e.Args[1];


            if (background.BackgroundObjects.CurrentIndex!=-1&&background.BackgroundObjects[ background.BackgroundObjects.CurrentIndex] is Structure structure)
            {
                double location = BveHacker.Scenario.VehicleLocation.Location;
                direct3DProvider.Device.SetTransform(SlimDX.Direct3D9.TransformState.World,
                Matrix.RotationY((float)totalElapse.TotalSeconds*data.Where(x=>x.FromLocation<location&&location<=x.ToLocation).FirstOrDefault()?.RotateRadPerSec??0)*matrix
                );
                structure.Model.Draw(direct3DProvider, false);
                structure.Model.Draw(direct3DProvider, true);
            }


            return new PatchInvokationResult(SkipModes.SkipOriginal);
        }


        public override void Dispose()
        {
            Patch.Dispose();
            Statements.LoadingCompleted -= Statements_LoadingCompleted;
        }
        TimeSpan totalElapse;
        public override void Tick(TimeSpan elapsed)
        {
            totalElapse += elapsed;
        }
    }
}
