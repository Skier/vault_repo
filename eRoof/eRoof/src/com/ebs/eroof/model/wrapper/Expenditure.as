package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.Expenditures_DTO;
	
	import flash.events.EventDispatcher;
	
	import mx.formatters.CurrencyFormatter;

	[Bindable]
	public class Expenditure extends EventDispatcher
	{
		public var expenditureDTO:Expenditures_DTO;
		
		private var _section:Section;
		public function get section():Section { return _section; }
		public function set section(value:Section):void 
		{
			_section = value;
		}
		
		public function get budgetYearStr():String
		{
			return expenditureDTO.BudgetYear.toString();
		}

		public function get typeOfWorkStr():String
		{
			return expenditureDTO.TypeOfWork;
		}

		public function get actionItemStr():String
		{
			return expenditureDTO.ActionItem;
		}

		public function get allocationStr():String
		{
			return expenditureDTO.Allocation;
		}

		public function get urgencyStr():String
		{
			return expenditureDTO.Urgency;
		}

		public function get statusStr():String
		{
			return expenditureDTO.Status;
		}

		public function get budgetCostStr():String
		{
			var cf:CurrencyFormatter = new CurrencyFormatter();
			cf.precision = 2;
			cf.useThousandsSeparator = true;

			return cf.format(expenditureDTO.Amount);
		}

		public function Expenditure(dto:Expenditures_DTO)
		{
			super(null);
			
			if (dto == null)
				throw new Error("Expenditure::Expenditure() - DTO object can not be null!");
			
			expenditureDTO = dto;
		}
		
	}
}