using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using Xceed.Wpf.AvalonDock.Themes;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace MissionEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        private int SelectDataRow { get; set; }
        private HashSet<int> WriteRows { get; } = new HashSet<int>();
        private readonly MissionLimitEditor _missionLimitEditor = new MissionLimitEditor();
        private readonly MissionTriggeredEditor _missionTriggeredEditor = new MissionTriggeredEditor();
        private readonly MissionResultEditor _missionResultEditor = new MissionResultEditor();
        private readonly MissionTaskBarEditor _missionTaskBarEditor = new MissionTaskBarEditor();
        private readonly MissionAnswerEditor _missionAnswerEditor = new MissionAnswerEditor();
        private readonly MissionExerciseEditor _missionExerciseEditor = new MissionExerciseEditor();
        private readonly MissionBattleAIEditor _missionBattleAiEditor = new MissionBattleAIEditor();

        private bool _isLayoutChanged = false;
        private bool _isExcelChanged = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadLayout();
            SetDataGridItemSource();
            MissionLimitPropertyGrid.SelectedObject = _missionLimitEditor;
            MissionTriggeredPropertyGrid.SelectedObject = _missionTriggeredEditor;
            MissionResultPropertyGrid.SelectedObject = _missionResultEditor;
            MissionTaskBarPropertyGrid.SelectedObject = _missionTaskBarEditor;
            MissionAnswerPropertyGrid.SelectedObject = _missionAnswerEditor;
            MissionExercisePropertyGrid.SelectedObject = _missionExerciseEditor;
            MissionBattleAIPropertyGrid.SelectedObject = _missionBattleAiEditor;
        }

        private void SetDataGridItemSource()
        {
            MissionDataGrid.ItemsSource = DatatableManager.Instance.Datatable.DefaultView;
        }

        private void ClearDataGridItemSource()
        {
            MissionDataGrid.ItemsSource = TempletManager.Instance.CreateTempletDataTable().DefaultView;
        }

        private void SetMissionLimitPropertyGrid()
        {
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["MinLevel"].ToString(),
                out int minLevel);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["MaxLevel"].ToString(),
                out int maxLevel);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["TransMinLevel"].ToString(),
                out int transMinLevel);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["TransMaxLevel"].ToString(),
                out int transMaxLevel);

            _missionLimitEditor.MinLevel = minLevel;
            _missionLimitEditor.MaxLevel = maxLevel;
            _missionLimitEditor.TransMinLevel = transMinLevel;
            _missionLimitEditor.TransMaxLevel = transMaxLevel;

            _missionLimitEditor.RequestMissionList = new List<int>();
            _missionLimitEditor.RequestRoleIDList = new List<int>();
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RequestMissionList" + i].ToString(),
                                    out int requestMission))
                {
                    _missionLimitEditor.RequestMissionList.Add(requestMission);
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RequestRoleIdList" + i].ToString(),
                    out int requestRoleId))
                {
                    _missionLimitEditor.RequestRoleIDList.Add(requestRoleId);
                }
                else
                {
                    break;
                }
            }
            MissionLimitPropertyGrid.Update();
        }

        private void SetMissionTriggeredPropertyGrid()
        {
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["MissionType"].ToString(),
                out int missionType);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoNpcID"].ToString(),
                out int activeInfoNpcID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoMapID"].ToString(),
                out int activeInfoMapID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoTeamState"].ToString(),
                out int activeInfoTeamState);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoLeftPos"].ToString(),
                out int activeInfoLeftPos);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoTopPos"].ToString(),
                out int activeInfoTopPos);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoRightPos"].ToString(),
                out int activeInfoRightPos);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoBottomPos"].ToString(),
                out int activeInfoBottomPos);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoTargetID"].ToString(),
                out int activeInfoTargetID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoTargetNum"].ToString(),
                out int activeInfoTargetNum);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoMiniStep"].ToString(),
                out int activeInfoMiniStep);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoStepProbability"].ToString(),
                out int activeInfoStepProbability);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoMaxStep"].ToString(),
                out int activeInfoMaxStep);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoTimeLimit"].ToString(),
                out int activeInfoTimeLimit);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoIsRestartTimer"].ToString(),
                out int activeInfoIsRestartTimer);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoGiveBackMoney"].ToString(),
                out int activeInfoGiveBackMoney);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoGiveBackPetID"].ToString(),
                out int activeInfoGiveBackPetID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoUseItemID"].ToString(),
                out int activeInfoUseItemID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ActiveInfoOtherType"].ToString(),
                out int activeInfoOtherType);

            _missionTriggeredEditor.MissionType = (MissionTriggeredEditor.Type)missionType;
            _missionTriggeredEditor.ActiveInfoNpcID = activeInfoNpcID;
            _missionTriggeredEditor.ActiveInfoMapID = activeInfoMapID;
            _missionTriggeredEditor.ActiveInfoTeamState = (MissionTriggeredEditor.TeamState)activeInfoTeamState;
            _missionTriggeredEditor.ActiveInfoLeftPos = activeInfoLeftPos;
            _missionTriggeredEditor.ActiveInfoTopPos = activeInfoTopPos;
            _missionTriggeredEditor.ActiveInfoRightPos = activeInfoRightPos;
            _missionTriggeredEditor.ActiveInfoBottomPos = activeInfoBottomPos;
            _missionTriggeredEditor.ActiveInfoTargetID = activeInfoTargetID;
            _missionTriggeredEditor.ActiveInfoTargetNum = activeInfoTargetNum;
            _missionTriggeredEditor.ActiveInfoMiniStep = activeInfoMiniStep;
            _missionTriggeredEditor.ActiveInfoStepProbability = activeInfoStepProbability;
            _missionTriggeredEditor.ActiveInfoMaxStep = activeInfoMaxStep;
            _missionTriggeredEditor.ActiveInfoTimeLimit = activeInfoTimeLimit;
            _missionTriggeredEditor.ActiveInfoIsRestartTimer = activeInfoIsRestartTimer;
            _missionTriggeredEditor.ActiveInfoGiveBackMoney = activeInfoGiveBackMoney;
            _missionTriggeredEditor.ActiveInfoGiveBackPetID = activeInfoGiveBackPetID;
            _missionTriggeredEditor.ActiveInfoUseItemID = activeInfoUseItemID;
            _missionTriggeredEditor.ActiveInfoOtherType = activeInfoOtherType;

            MissionTriggeredPropertyGrid.Update();
        }

        private void SetMissionResultPropertyGrid()
        {
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["TransformID"].ToString(),
                out int transformID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ExpReward"].ToString(),
                out int expReward);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["MoneyReward"].ToString(),
                out int moneyReward);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["PetExpReward"].ToString(),
                out int petExpReward);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["SMoney"].ToString(),
                out int sMoney);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ShengWang"].ToString(),
                out int shengWang);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RewardMapJumpType"].ToString(),
                out int rewardMapJumpType);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ProcessBarTime"].ToString(),
                out int processBarTime);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RewardMapID"].ToString(),
                out int rewardMapID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ProcessBarText"].ToString(),
                out int processBarText);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RewardMapXPos"].ToString(),
                out int rewardMapXPos);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ProcessBarColor"].ToString(),
                out int processBarColor);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["RewardMapYPos"].ToString(),
                out int rewardMapYPos);

            _missionResultEditor.PostMissionList = new List<int>();
            _missionResultEditor.DisPlayNPCID = new List<int>();
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["PostMissionList" + i].ToString(),
                    out int postMission))
                {
                    _missionResultEditor.PostMissionList.Add(postMission);
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["DisPlayNPCID" + i].ToString(),
                    out int disPlayNPCID))
                {
                    _missionResultEditor.DisPlayNPCID.Add(disPlayNPCID);
                }
                else
                {
                    break;
                }
            }
            _missionResultEditor.TransformID = transformID;
            _missionResultEditor.NoteInfo = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["NoteInfo"].ToString();
            _missionResultEditor.ExpReward = expReward;
            _missionResultEditor.MoneyReward = moneyReward;
            _missionResultEditor.PetExpReward = petExpReward;
            _missionResultEditor.SMoney = sMoney;
            _missionResultEditor.ShengWang = shengWang;
            _missionResultEditor.RewardMapJumpType = (MissionResultEditor.JumpType)rewardMapJumpType;
            _missionResultEditor.ProcessBarTime = processBarTime;
            _missionResultEditor.RewardMapID = rewardMapID;
            _missionResultEditor.ProcessBarText = processBarText;
            _missionResultEditor.RewardMapXPos = rewardMapXPos;
            _missionResultEditor.ProcessBarColor = processBarColor;
            _missionResultEditor.RewardMapYPos = rewardMapYPos;

            MissionResultPropertyGrid.Update();
        }

        private void SetMissionTaskBarPropertyGrid()
        {
            _missionTaskBarEditor.TaskInfoDescriptionListA = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["TaskInfoDescriptionListA"].ToString();
            _missionTaskBarEditor.TaskInfoPurposeListA = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["TaskInfoPurposeListA"].ToString();
            _missionTaskBarEditor.TaskInfoTraceListA = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["TaskInfoTraceListA"].ToString();

            MissionTaskBarPropertyGrid.Update();
        }

        private void SetMissionAnswerPropertyGrid()
        {
            _missionAnswerEditor.QuestionInfoCorrectAnswer = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["QuestionInfoCorrectAnswer"].ToString();
            _missionAnswerEditor.QuestionInfoWrongAnswerList0 = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["QuestionInfoWrongAnswerList0"].ToString();
            _missionAnswerEditor.QuestionInfoWrongAnswerList1 = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["QuestionInfoWrongAnswerList1"].ToString();
            _missionAnswerEditor.QuestionInfoWrongAnswerList2 = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["QuestionInfoWrongAnswerList2"].ToString();
            _missionAnswerEditor.QuestionInfoWrongAnswerList3 = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["QuestionInfoWrongAnswerList3"].ToString();
            _missionAnswerEditor.QuestionInfoWrongAnswerList4 = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["QuestionInfoWrongAnswerList4"].ToString();

            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["QuestionInfoConversion"].ToString(),
                out int questionInfoConversion);
            _missionAnswerEditor.QuestionInfoConversion = questionInfoConversion;

            MissionAnswerPropertyGrid.Update();
        }

        private void SetMissionExercisePropertyGrid()
        {
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["BattleInfoBattleMapType"].ToString(),
                out int battleInfoBattleMapType);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["BattleInfoBattleZoneID"].ToString(),
                out int battleInfoBattleZoneID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["BattleInfoDrop"].ToString(),
                out int battleInfoDrop);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["BattleInfoBattleTimes"].ToString(),
                out int battleInfoBattleTimes);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["BattleInfoMonsterNum"].ToString(),
                out int battleInfoMonsterNum);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["BattleInfoDropItemID"].ToString(),
                out int battleInfoDropItemID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["BattleInfoDropItemNum"].ToString(),
                out int battleInfoDropItemNum);
            _missionExerciseEditor.BattleInfoBattleMapType = (MissionExerciseEditor.MapType)battleInfoBattleMapType;
            _missionExerciseEditor.BattleInfoBattleZoneID = battleInfoBattleZoneID;
            _missionExerciseEditor.BattleInfoDrop = battleInfoDrop;
            _missionExerciseEditor.BattleInfoBattleTimes = battleInfoBattleTimes;
            _missionExerciseEditor.BattleInfoMonsterNum = battleInfoMonsterNum;
            _missionExerciseEditor.BattleInfoDropItemID = battleInfoDropItemID;
            _missionExerciseEditor.BattleInfoDropItemNum = battleInfoDropItemNum;

            for (int i = 0; i < 50; i++)
            {
                if (int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["BattleInfoMonsterList" + i].ToString(),
                    out int battleInfoMonster))
                {
                    _missionExerciseEditor.BattleInfoMonsterList.Add(battleInfoMonster);
                }
                else
                {
                    break;
                }
            }

            MissionExercisePropertyGrid.Update();
        }

        private void SetMissionBattleAIPropertyGrid()
        {
            int.TryParse(GetSelectData("AIInfoAIID"), out int aiInfoAIID);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["AIInfoTeamSteate"].ToString(),
                out int aiInfoTeamSteate);
            int.TryParse(DatatableManager.Instance.Datatable.Rows[SelectDataRow]["AIInfoDeathPunish"].ToString(),
                out int aiInfoDeathPunish);

            _missionBattleAiEditor.AIInfoAIID = aiInfoAIID;
            _missionBattleAiEditor.AIInfoTeamSteate = (MissionBattleAIEditor.TeamSteate)aiInfoTeamSteate;
            _missionBattleAiEditor.AIInfoDeathPunish = (MissionBattleAIEditor.DeathPunish)aiInfoDeathPunish;
            _missionBattleAiEditor.AIInfoBattleLevel = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["AIInfoBattleLevel"].ToString();

            MissionBattleAIPropertyGrid.Update();
        }

        private string GetSelectData(string column) => DatatableManager.Instance.Datatable.Rows[SelectDataRow][column].ToString();

        private void SetConversationCell()
        {
            ScenarioInfoNpcConversationListBox.Items.Clear();
            for (int i = 0; i < 50; i++)
            {
                if (!int.TryParse(
                    DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ScenarioInfoNpcID" + i].ToString(),
                    out int npcId)) continue;
                string conversation = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ScenarioInfoNpcConversationList" + i].ToString();
                ConfigManager.Instance.GetNpcInfo(npcId, out string npcName, out BitmapSource headBitmapSource);
                ScenarioInfoNpcConversationListBox.Items.Add(new ConversationCell(npcName, headBitmapSource, conversation, ScenarioInfoNpcConversationListBox.ActualWidth));
            }
        }

        private void SetFinishConversationCell()
        {
            ScenarioInfoFinishConversationListBox.Items.Clear();
            for (int i = 0; i < 50; i++)
            {
                if (!int.TryParse(
                    DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ScenarioInfoFinishNpcID" + i].ToString(),
                    out int npcId)) continue;
                string conversation = DatatableManager.Instance.Datatable.Rows[SelectDataRow]["ScenarioInfoFinishConversationList" + i].ToString();
                ConfigManager.Instance.GetNpcInfo(npcId, out string npcName, out BitmapSource headBitmapSource);
                ScenarioInfoFinishConversationListBox.Items.Add(new ConversationCell(npcName, headBitmapSource, conversation, ScenarioInfoFinishConversationListBox.ActualWidth));
            }
        }

        private void MissionDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectDataRow = MissionDataGrid.SelectedIndex;
            WriteRows.Add(SelectDataRow);

            MissionLimitPropertyGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(SetMissionLimitPropertyGrid));
            MissionTriggeredPropertyGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(SetMissionTriggeredPropertyGrid));
            MissionResultPropertyGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(SetMissionResultPropertyGrid));
            MissionResultPropertyGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(SetMissionTaskBarPropertyGrid));
            MissionResultPropertyGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(SetMissionAnswerPropertyGrid));
            MissionResultPropertyGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(SetMissionExercisePropertyGrid));
            MissionResultPropertyGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(SetMissionBattleAIPropertyGrid));
            MissionResultPropertyGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(SetConversationCell));
            MissionResultPropertyGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(SetFinishConversationCell));
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
        }

        private void SaveLayout()
        {
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(MissionDockingManager);
            using (var writer = new StreamWriter("MissionEditor.layout"))
            {
                layoutSerializer.Serialize(writer);
            }
        }

        private void LoadLayout()
        {
            if (!File.Exists("MissionEditor.layout")) return;

            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(MissionDockingManager);
            using (var reader = new StreamReader("MissionEditor.layout"))
            {
                layoutSerializer.Deserialize(reader);
            }
        }

        private void LoadLayout(string loadPath)
        {
            if (!File.Exists(loadPath)) return;

            InitializeComponent();

            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(MissionDockingManager);
            using (var reader = new StreamReader(loadPath))
            {
                layoutSerializer.Deserialize(reader);
            }
        }

        private void DataGridSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchedDataRows = from dataRow in DatatableManager.Instance.Datatable.AsEnumerable()
                                   where dataRow["MissionID"].ToString().Contains(DataGridSearchTextBox.Text)
                                      || dataRow["MissionName"].ToString().Contains(DataGridSearchTextBox.Text)
                                      || dataRow["MissionTypeString"].ToString().Contains(DataGridSearchTextBox.Text)
                                   select DatatableManager.Instance.Datatable.Rows.IndexOf(dataRow);
            object item = MissionDataGrid.Items[searchedDataRows.FirstOrDefault()];
            MissionDataGrid.SelectedItem = item;
            MissionDataGrid.ScrollIntoView(item);
        }

        private void NewButton_OnClick(object sender, RoutedEventArgs e)
        {
            ClearDataGridItemSource();
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Microsoft Excel文件|*.xlsx",
                RestoreDirectory = false
            };
            DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            ConfigManager.Instance.MissionFilePath = openFileDialog.FileName.Trim();
            SetDataGridItemSource();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            DatatableManager.Instance.SaveExcel(WriteRows);
        }

        private void SetButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExportGbeans_OnClick(object sender, RoutedEventArgs e)
        {
            TempletManager.Instance.CreateGBeansFile();
        }

        private void SaveLayoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            SaveLayout();
        }

        private void LoadLayoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "layout|*.layout",
                RestoreDirectory = false
            };
            DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            LoadLayout(openFileDialog.FileName);
        }

        private void RecoverLayoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            File.Delete("MissionEditor.layout");
            if (MessageBox.Show(this, "重置布局完成，是否重启MissionEditor？", "MissionEditor", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var fileName = System.Reflection.Assembly.GetExecutingAssembly().Location;

                if (fileName != null)
                    Process.Start(fileName);

                Application.Current.Shutdown();
            }
        }

        private void CheckBox1_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckBox1.IsChecked == null)
            {
                return;
            }
            bool isShowSortOptions = (bool)CheckBox1.IsChecked;
            MissionLimitPropertyGrid.ShowSortOptions = isShowSortOptions;
            MissionTriggeredPropertyGrid.ShowSortOptions = isShowSortOptions;
            MissionResultPropertyGrid.ShowSortOptions = isShowSortOptions;
            MissionTaskBarPropertyGrid.ShowSortOptions = isShowSortOptions;
            MissionAnswerPropertyGrid.ShowSortOptions = isShowSortOptions;
            MissionExercisePropertyGrid.ShowSortOptions = isShowSortOptions;
            MissionBattleAIPropertyGrid.ShowSortOptions = isShowSortOptions;
        }

        private void CheckBox2_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckBox2.IsChecked == null)
            {
                return;
            }
            bool isShowSummary = (bool)CheckBox2.IsChecked;
            MissionLimitPropertyGrid.ShowSummary = isShowSummary;
            MissionTriggeredPropertyGrid.ShowSummary = isShowSummary;
            MissionResultPropertyGrid.ShowSummary = isShowSummary;
            MissionTaskBarPropertyGrid.ShowSummary = isShowSummary;
            MissionAnswerPropertyGrid.ShowSummary = isShowSummary;
            MissionExercisePropertyGrid.ShowSummary = isShowSummary;
            MissionBattleAIPropertyGrid.ShowSummary = isShowSummary;
        }

        private void CheckBox3_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckBox3.IsChecked == null)
            {
                return;
            }
            bool isShowSearchBox = (bool)CheckBox3.IsChecked;
            MissionLimitPropertyGrid.ShowSearchBox = isShowSearchBox;
            MissionTriggeredPropertyGrid.ShowSearchBox = isShowSearchBox;
            MissionResultPropertyGrid.ShowSearchBox = isShowSearchBox;
            MissionTaskBarPropertyGrid.ShowSearchBox = isShowSearchBox;
            MissionAnswerPropertyGrid.ShowSearchBox = isShowSearchBox;
            MissionExercisePropertyGrid.ShowSearchBox = isShowSearchBox;
            MissionBattleAIPropertyGrid.ShowSearchBox = isShowSearchBox;
        }

        private void ToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ToggleButton.IsChecked == null)
            {
                return;
            }
            bool isCategorized = (bool)ToggleButton.IsChecked;
            MissionLimitPropertyGrid.IsCategorized = isCategorized;
            MissionTriggeredPropertyGrid.IsCategorized = isCategorized;
            MissionResultPropertyGrid.IsCategorized = isCategorized;
            MissionTaskBarPropertyGrid.IsCategorized = isCategorized;
            MissionAnswerPropertyGrid.IsCategorized = isCategorized;
            MissionExercisePropertyGrid.IsCategorized = isCategorized;
            MissionBattleAIPropertyGrid.IsCategorized = isCategorized;
        }

        private void AeroTheme_OnClick(object sender, RoutedEventArgs e)
        {
            MissionDockingManager.Theme = new AeroTheme();
        }

        private void MetroTheme_OnClick(object sender, RoutedEventArgs e)
        {
            MissionDockingManager.Theme = new MetroTheme();
        }

        private void GenericTheme_OnClick(object sender, RoutedEventArgs e)
        {
            MissionDockingManager.Theme = new GenericTheme();
        }

        private void VSTheme_OnClick(object sender, RoutedEventArgs e)
        {
            MissionDockingManager.Theme = new VS2010Theme();
        }
    }
}