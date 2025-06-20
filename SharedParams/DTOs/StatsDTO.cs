using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedParams.DTOs
{
	public class StatsDTO
	{
		public decimal TotalDepenser { get; set; } = 0;
		public decimal TotalRevenue { get; set; } = 0;
		public decimal TotalKeep { get; set; } = 0;

		public decimal Profit => TotalRevenue - TotalDepenser;

	}
}
