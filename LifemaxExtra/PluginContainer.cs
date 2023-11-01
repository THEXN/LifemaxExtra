using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace LifemaxExtra
{
    [ApiVersion(2, 1)]

    public class LifemaxExtra : TerrariaPlugin
    {
        public override string Author => "佚名，肝帝熙恩添加自定义";
        public override string Description => "提升生命值上限";
        public override string Name => "LifemaxExtra";
        public override Version Version => new Version(1, 0, 0, 2);
        public static Configuration Config;
        public LifemaxExtra(Main game) : base(game)
        {
            LoadConfig();
            this.controlUseItemOld = new bool[255];
            this.itemUseTime = new int[255];
        }

        private static void LoadConfig()
        {
            Config = Configuration.Read(Configuration.FilePath);
            Config.Write(Configuration.FilePath);

        }
        private static void ReloadConfig(ReloadEventArgs args)
        {
            LoadConfig();
            args.Player?.SendSuccessMessage("[{0}] 重新加载配置完毕。", typeof(LifemaxExtra).Name);
        }

        public override void Initialize()
        {
            GeneralHooks.ReloadEvent += ReloadConfig;
            ServerApi.Hooks.GameUpdate.Register(this, new HookHandler<EventArgs>(this.OnUpdate));
            PlayerHooks.PlayerPostLogin += OnPlayerPostLogin;
        }

        private void OnPlayerPostLogin(PlayerPostLoginEventArgs args)
        {
            foreach (TSPlayer tsplayer in TShock.Players)
            {
                if (tsplayer != null)
                {
                    // 检查生命上限并设置生命值
                    CheckAndSetPlayerHealth(tsplayer);
                }
            }
        }


        private static void CheckAndSetPlayerHealth(TSPlayer tsplayer)
        {
            int index = tsplayer.Index;
            Player tplayer = tsplayer.TPlayer;

            // 如果生命上限大于配置的最大值
            if (tplayer.statLifeMax > Config.使用生命果最高可提升至)
            {
                // 将生命值设置为配置的最大值
                tplayer.statLifeMax = Config.使用生命果最高可提升至;
                tsplayer.SendData((PacketTypes)16, "", index, 0f, 0f, 0f, 0);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                PlayerHooks.PlayerPostLogin -= OnPlayerPostLogin;
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
                                if (tplayer.statLifeMax >= Config.使用生命水晶最高可提升至)
                                {
                                    if(tsplayer.TPlayer.statLifeMax < Config.使用生命果最高可提升至)
                                    {
                                        tsplayer.TPlayer.inventory[tsplayer.TPlayer.selectedItem].stack--;
                                        tsplayer.SendData((PacketTypes)5, "", index, (float)tplayer.selectedItem, 0f, 0f, 0);
                                        tplayer.statLifeMax += 5;
                                        tsplayer.SendData((PacketTypes)16, "", index, 0f, 0f, 0f, 0);

                                    }
                                    if (tsplayer.TPlayer.statLifeMax > Config.使用生命果最高可提升至)
                                    {
                                        tsplayer.TPlayer.inventory[tsplayer.TPlayer.selectedItem].stack--;
                                        tsplayer.SendData((PacketTypes)5, "", index, (float)tplayer.selectedItem, 0f, 0f, 0);
                                        tplayer.statLifeMax = Config.使用生命果最高可提升至;
                                        tsplayer.SendData((PacketTypes)16, "", index, 0f, 0f, 0f, 0);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (tplayer.statLifeMax >= 400)
                            {
                                if(tsplayer.TPlayer.statLifeMax < Config.使用生命水晶最高可提升至)
                                {
                                    tsplayer.TPlayer.inventory[tplayer.selectedItem].stack--;
                                    tsplayer.SendData((PacketTypes)5, "", index, (float)tplayer.selectedItem, 0f, 0f, 0);
                                    tplayer.statLifeMax += 20;
                                    tsplayer.SendData((PacketTypes)16, "", index, 0f, 0f, 0f, 0);
                                }
                                else if(tsplayer.TPlayer.statLifeMax > Config.使用生命水晶最高可提升至 && tsplayer.TPlayer.statLifeMax < Config.使用生命果最高可提升至)
                                {
                                    tsplayer.TPlayer.inventory[tsplayer.TPlayer.selectedItem].stack--;
                                    tsplayer.SendData((PacketTypes)5, "", index, (float)tplayer.selectedItem, 0f, 0f, 0);
                                    tplayer.statLifeMax = Config.使用生命水晶最高可提升至;
                                    tsplayer.SendData((PacketTypes)16, "", index, 0f, 0f, 0f, 0);
                                }
                            }
                        }
                    }
                    this.controlUseItemOld[index] = tsplayer.TPlayer.controlUseItem;
                }
            }
        }

        private bool disposed;

        private bool[] controlUseItemOld;

        private int[] itemUseTime;
    }
}
