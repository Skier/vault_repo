using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;

namespace MobileTech.Windows.UI
{
	public enum CommandName
	{
		StartDay,
		DatabaseManager,
		EndDay,
		CustomerOperations,
		CustomerSelection,
		Invoice,
		InvoiceItemEntry,
        CustomerOperationsCommit,
		Events,
		MainMenu,
        TComm,
        InventoryMenu,
        InventoryLoad,
        InventoryUnload,
        Information,
        ItemSearch,
        SetupMenu,
        SetupRoute,
        SelectItem,
        Odometer,
        Category,
        InventoryUnloadCommit,
        InventoryUnloadGood,
        InventoryUnloadDamage,
        InventoryUnloadEquipment,
        InventoryLoadCommit,
        InventoryLoadGood,
        InventoryLoadEquipment,
        InventoryLoadDamage,
        Password
	}

    public enum PasswordName
    {
        StartDay,
        InventoryLoad,
        InventoryUnload,
        SetupRoute,
        EndDay,
        ChangeDate,
        Exit
    }

	public interface IApplication
	{
		/// <summary>
		/// Async call, model will be find by application
		/// </summary>
		/// <param name="command"></param>
        /// <exception cref="MobileTech.MobileTechAccessDeniedException"></exception>
        [ExceptionHandlerRequiredAttribute]
		void Execute(CommandName command);
		/// <summary>
		/// Async call with passed model
		/// </summary>
		/// <param name="command"></param>
		/// <param name="model"></param>
        /// <exception cref="MobileTech.MobileTechAccessDeniedException"></exception>
        [ExceptionHandlerRequiredAttribute]
        void Execute(CommandName command, Object data);
		/// <summary>
		/// Async or Sync call with passed model
		/// </summary>
		/// <param name="command"></param>
		/// <param name="model"></param>
		/// <param name="async"></param>
        /// <exception cref="MobileTech.MobileTechAccessDeniedException"></exception>
        [ExceptionHandlerRequiredAttribute]
        void Execute(CommandName command, Object data, bool async);

        [Obsolete("For sync call please use Execute(CommandName command, Object data, bool async) function "+
          "because this function conflicted with void Execute(CommandName command, Object data)", true)]
        void Execute(CommandName command, bool async);
	}
}
