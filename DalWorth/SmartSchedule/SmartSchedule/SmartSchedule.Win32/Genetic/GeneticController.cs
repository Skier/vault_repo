using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPivotGrid;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.Genetic;
using SmartSchedule.Win32.HistoricalOrders;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.Genetic
{
    public class GeneticController : Controller<GeneticModel, GeneticView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion        

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.BookingEngine = (BookingEngine) data[0];
            base.OnModelInitialize(data);
        }

        #endregion


        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnClose.Click += OnCancelClick;
            View.m_btnRun.Click += OnRunClick;
            View.m_btnDisplay.Click += OnDisplayClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            Fitness fitness = Model.GetInitialFitnessValue();
            View.m_lblDriveInitial.Text = fitness.DriveDistance.ToString("0.00");
            View.m_lblSecAreaVisitsInitial.Text = fitness.VisitsInSecondaryArea.ToString();
            View.m_lblBalanceInitial.Text = fitness.BalanceStandardDeviation.ToString("0.00");
            View.m_lblTempAssignedVisitsInitial.Text = fitness.TemporaryAssignedVisits.ToString();

            View.m_btnDisplay.Enabled = false;
        }

        #endregion

        #region OnRunClick

        private void OnRunClick(object sender, EventArgs e)
        {           
            Population population;
            int generationsCount;

            try
            {
                Utils.SecondaryAreaToMileMultiplier = int.Parse(View.m_txtSecondaryAreaMultiplier.Text);
                Utils.BalanceToMileMultiplier = double.Parse(View.m_txtBalanceMultiplier.Text) / 100;
                Utils.TempAssignmentToMileMultiplier = double.Parse(View.m_txtTempAssignmentMultiplier.Text);

                generationsCount = int.Parse(View.m_txtGenerationsCount.Text);

                population = new Population(Model.BookingEngine, 
                    int.Parse(View.m_txtPopulationSize.Text),
                    int.Parse(View.m_txtSelectionSize.Text),
                    int.Parse(View.m_txtChildrenToProduce.Text),
                    int.Parse(View.m_txtMutationsCount.Text),
                    int.Parse(View.m_txtMutationStrength.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong parameter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            population.InitialChromosomeGenerated += OnInitialChromosomeGenerated;

            View.m_btnDisplay.Enabled = false;

            using (new WaitCursor())
            {
                View.m_lblstatus.Text = "Generating initial population... ";
                View.m_lblstatus.Visible = true;
                Application.DoEvents();
                population.FillRandom();
                View.m_lblstatus.Visible = false;

                for (int i = 1; i <= generationsCount; i++)
                {
                    population.NextGeneration();
                    View.m_lblCurrentGeneration.Text = i.ToString();

                    Fitness fitness = population.BestChromosome.FitnessValue;
                    View.m_lblDriveCurrent.Text = fitness.DriveDistance.ToString("0.00");
                    View.m_lblSecAreaVisitsCurrent.Text = fitness.VisitsInSecondaryArea.ToString();
                    View.m_lblBalanceCurrent.Text = fitness.BalanceStandardDeviation.ToString("0.00");
                    View.m_lblTempAssignedVisitsCurrent.Text = fitness.TemporaryAssignedVisits.ToString();
                    
                    Application.DoEvents();
                }
            }

            Model.CurrentSolution = population.BestChromosome;
            View.m_btnDisplay.Enabled = true;
        }

        private void OnInitialChromosomeGenerated(int totalChromosomesGenerated)
        {
            View.m_lblstatus.Text = "Generating initial population... " + totalChromosomesGenerated;
            Application.DoEvents();
        }

        #endregion

        #region OnDisplayClick

        private void OnDisplayClick(object sender, EventArgs e)
        {
            if (Model.CurrentSolution == null)
                return;

            int initialVisitsCount = Model.BookingEngine.Visits.Count;

            try
            {
                using (new WaitCursor())
                {
                    Database.Begin();
                    Model.CurrentSolution.Display();
                    Database.Commit();                    
                }
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            MessageBox.Show("Visits count old = " + initialVisitsCount
                + ", Visits count new = " + Model.BookingEngine.Visits.Count);
            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion
   
    }
}
