using System.Collections.Generic;
using System.IO;
using Kesmai.Server.Game;
using Kesmai.Server.Network;
using Kesmai.Server.Spells;

namespace Kesmai.Server.Items
{
	public partial class SharkJacket : Jacket
	{
		/// <inheritdoc />
		public override uint BasePrice => 20;

        /// <inheritdoc />
		public override int Weight => 1200;

        /// <inheritdoc />
		public override int Hindrance => 0;

        /// <summary>
		/// Initializes a new instance of the <see cref="SharkJacket"/> class.
		/// </summary>
		public SharkJacket() : base(270)
		{
		}

        /// <inheritdoc />
		public override void GetDescription(List<LocalizationEntry> entries)
		{
			entries.Add(new LocalizationEntry(6200000, 6200179)); /* [You are looking at] [a jacket made from the skin of a shark.] */

			if (Identified)
				entries.Add(new LocalizationEntry(6250099)); /* The jacket appears quite ordinary. */
		}

		protected override bool OnEquip(MobileEntity entity)
		{
			if (!base.OnEquip(entity))
				return false;

			if (!entity.GetStatus(typeof(BreatheWaterStatus), out var status))
			{
				status = new BreatheWaterStatus(entity)
				{
					Inscription = new SpellInscription() { SpellId = 4 }
				};
				status.AddSource(new ItemSource(this));
				
				entity.AddStatus(status);
			}
			else
			{
				status.AddSource(new ItemSource(this));
			}

			return true;
		}

		protected override bool OnUnequip(MobileEntity entity)
		{
			if (!base.OnUnequip(entity))
				return false;

			if (entity.GetStatus(typeof(BreatheWaterStatus), out var status))
				status.RemoveSourceFor(this);

			return true;
		}
	}
}
