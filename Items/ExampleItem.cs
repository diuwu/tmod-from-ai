using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Items
{
    public class ExampleItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("倚天剑");
            Tooltip.SetDefault("这是一把强大的剑。");
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
            return base.UseItem(player);
        }

        public override bool MeleePrefix(Player player, ref float speedMult, ref float knockbackMult, ref float useTimeMult, ref float damageMult, ref float scaleMult, ref int crit)
        {
            return base.MeleePrefix(player, ref speedMult, ref knockbackMult, ref useTimeMult, ref damageMult, ref scaleMult, ref crit);
        }

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DaylightFragment"), 10); // 修改配方材料
            recipe.AddIngredient(mod.ItemType("StardustFragment"), 10); // 修改配方材料
            recipe.AddIngredient(mod.ItemType("StarmetalFragment"), 10); // 修改配方材料
            recipe.AddIngredient(mod.ItemType("StardustCore"), 10); // 修改配方材料
            recipe.AddTile(mod.TileType("AncientManipulator")); // 修改配方台
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool SwingThroughTiles => true;

        public override bool? CanHitNPC(Player player, NPC target)
        {
            return base.CanHitNPC(player, target);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
            }
        }

        public override bool PreSwing(Player player, Rectangle hitbox)
        {
            return base.PreSwing(player, hitbox);
        }

        public override void PostSwing(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
            }
        }

        public override bool CanSwing(Player player)
        {
            return base.CanSwing(player);
        }

        public override bool? UseItemFrame(Player player)
        {
            // 自定义挥砍动画
            if (player.itemAnimation == 36 || player.itemAnimation == 18)
            {
                player.itemRotation = 0.785f; // 横砍角度
                // 发射白色半透明的剑气弹幕
                if (player.itemAnimation == 36) // 第一次横砍
                {
                    ShootSwordBeam(player, 0.785f);
                }
                else if (player.itemAnimation == 18) // 第二次横砍
                {
                    ShootSwordBeam(player, 0.785f);
                }
            }
            else if (player.itemAnimation == 27 || player.itemAnimation == 9)
            {
                player.itemRotation = 1.57f; // 竖砍角度
                // 发射白色半透明的剑气弹幕
                if (player.itemAnimation == 27) // 第一次竖砍
                {
                    ShootSwordBeam(player, 1.57f);
                }
                else if (player.itemAnimation == 9) // 第二次竖砍
                {
                    ShootSwordBeam(player, 1.57f);
                }
            }
            return true;
        }

        private void ShootSwordBeam(Player player, float rotation)
        {
            Vector2 position = player.Center + Vector2.Normalize(new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation))) * 40f;
            Vector2 velocity = Vector2.Normalize(new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation))) * 10f;
            Projectile.NewProjectile(position, velocity, mod.ProjectileType("SwordBeam"), item.damage, item.knockBack, player.whoAmI, 0f, 0f);
        }
    }
}