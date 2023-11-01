using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Configuration;
using TShockAPI.Hooks;

namespace LifemaxExtra
{
    // Token: 0x02000005 RID: 5
    [ApiVersion(2, 1)]

	public class LifemaxExtra : TerrariaPlugin
	{
        public override string Author => "佚名";
        public override string Description => "提升生命值上限";
        public override string Name => "LifemaxExtra";
        public override Version Version => new Version(1, 0, 0, 0);
        public static ConfigFile Config { get; set; }
        public LifemaxExtra(Main game) : base(game)
        {
            GeneralHooks.ReloadEvent += delegate (ReloadEventArgs _)
            {
                this.LoadConfig();
            };
            this.controlUseItemOld = new bool[255];
            this.itemUseTime = new int[255];
        }
        internal static string CPath = Path.Combine(TShock.SavePath, "LifemaxExtra.json");
        #region config
        public void LoadConfig()
        {
            try
            {
                if (File.Exists(CPath))
                {
                    Config = ConfigFile.Read(CPath);// 读取配置并且补全配置
                }
                else
                {
                    TShock.Log.ConsoleError("创建配置文件");
                }
                Config.Write(CPath);
            }
            catch (Exception ex)
            {
                TShock.Log.ConsoleError("插件错误配置读取错误:" + ex.ToString());
            }
        }
        #endregion

		// Token: 0x06000006 RID: 6 RVA: 0x000020BD File Offset: 0x000002BD
		public override void Initialize()
		{
            this.LoadConfig();
			ServerApi.Hooks.GameUpdate.Register(this, new HookHandler<EventArgs>(this.OnUpdate));
        }

		// Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				ServerApi.Hooks.GameUpdate.Deregister(this, new HookHandler<EventArgs>(this.OnUpdate));
			}
			base.Dispose(disposing);
		}
        private void OnUpdate(EventArgs args)
		{
            foreach (TSPlayer tsplayer in TShock.Players)
			{

				if (!(tsplayer == null))
				{
					int index = tsplayer.Index;
					Player tplayer = tsplayer.TPlayer;
					Item heldItem = tplayer.HeldItem;
                    if (!this.controlUseItemOld[index] && tsplayer.TPlayer.controlUseItem && this.itemUseTime[index] <= 0)
                    {
                        int useTime = heldItem.useTime;
                        int type = heldItem.type;

                        if (type != 29)
                        {
                            if (type == 1291)
                            {
                                if (tsplayer.TPlayer.statLifeMax < Config.生命果最高使用值 && tplayer.statLifeMax >= Config.生命果最低使用值)
                                {
                                    tsplayer.TPlayer.inventory[tsplayer.TPlayer.selectedItem].stack--;
                                    tsplayer.SendData((PacketTypes)5, "", index, (float)tplayer.selectedItem, 0f, 0f, 0);
                                    tplayer.statLifeMax += 5;
                                    tsplayer.SendData((PacketTypes)16, "", index, 0f, 0f, 0f, 0);
                                }
                            }
                        }
                        else
                        {
                            if (tsplayer.TPlayer.statLifeMax < Config.生命水晶最高使用值 && tplayer.statLifeMax >= Config.生命水晶最低使用值)
                            {
                                tsplayer.TPlayer.inventory[tplayer.selectedItem].stack--;
                                tsplayer.SendData((PacketTypes)5, "", index, (float)tplayer.selectedItem, 0f, 0f, 0);
                                tplayer.statLifeMax += 20;
                                tsplayer.SendData((PacketTypes)16, "", index, 0f, 0f, 0f, 0);
                            }
                        }
                    }
                    this.controlUseItemOld[index] = tsplayer.TPlayer.controlUseItem;
				}
			}
		}

		// Token: 0x04000003 RID: 3
		private bool disposed;

		// Token: 0x04000004 RID: 4
		private bool[] controlUseItemOld;

		// Token: 0x04000005 RID: 5
		private int[] itemUseTime;
    }
}
