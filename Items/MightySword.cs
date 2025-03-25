using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Items
{
    public class MightySword : ModItem // 修改类名为MightySword
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("倚天剑");
            Tooltip.SetDefault("召唤一把强大的剑。");
        }

        public override void SetDefaults()
        {
            item.width = 40; // 修改宽度以适应武器
            item.height = 40; // 修改高度以适应武器
            item.maxStack = 1; // 武器通常不堆叠
            item.value = 10000; // 提高物品价值
            item.rare = ItemRarityID.Purple; // 提高物品稀有度
            item.useAnimation = 36; // 使用动画时间
            item.useTime = 36; // 使用时间
            item.useStyle = ItemUseStyleID.SwingThrow; // 使用方式
            item.autoReuse = true; // 自动重用
            item.damage = 50; // 增加武器伤害
            item.knockBack = 6f; // 增加击退力
            item.melee = true; // 设置为近战武器
            item.useTurn = false; // 不需要转身使用
            item.useTexture = "Terraria/Item_53"; // 使用原版泰拉刃的贴图
        }

        public override bool UseItem(Player player)
        {
            // 检查玩家是否有标记敌人
            NPC target = GetMarkedNPC(player);
            if (target != null)
            {
                // 瞬移到敌人身边进行挥砍
                player.Teleport(target.Center);
                // 触发挥砍动画
                player.itemAnimation = item.useAnimation;
                player.itemTime = item.useTime;
                player.itemLocation = target.Center;
            }
            else
            {
                // 发射剑气攻击
                ShootSwordBeam(player);
            }
            return true;
        }

        private NPC GetMarkedNPC(Player player)
        {
            // 假设玩家使用鞭子武器命中敌人时会标记敌人，这里需要根据实际情况实现
            // 这里仅作示例，假设标记敌人的方式是通过一个自定义的Buff
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.HasBuff(BuffID.Daybreak)) // 修改为泰拉瑞亚原版的Daybreak Buff
                {
                    return npc;
                }
            }
            return null;
        }

        private void ShootSwordBeam(Player player)
        {
            Vector2 position = player.Center;
            Vector2 velocity = player.DirectionTo(Main.MouseWorld) * 10f;
            Projectile.NewProjectile(position, velocity, mod.ProjectileType("SwordBeam"), item.damage, item.knockBack, player.whoAmI, 0f, 0f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 10); // 修改配方材料为10个铁锭
            recipe.AddTile(TileID.Anvils); // 修改配方台为铁砧
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            // 添加标记Buff
            if (player.inventory[player.selectedItem].ammo == AmmoID.Whip) // 检查是否使用鞭子武器
            {
                target.AddBuff(BuffID.Daybreak, 300); // 修改为泰拉瑞亚原版的Daybreak Buff
            }
        }
    }
}