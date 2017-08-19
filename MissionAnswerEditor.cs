using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    public class MissionAnswerEditor
    {
        [DisplayName("正确答案")]
        [PropertyOrder(1)]
        public string QuestionInfoCorrectAnswer { get; set; }

        [DisplayName("错误选项1")]
        [PropertyOrder(2)]
        public string QuestionInfoWrongAnswerList0 { get; set; }

        [DisplayName("错误选项2")]
        [PropertyOrder(3)]
        public string QuestionInfoWrongAnswerList1 { get; set; }

        [DisplayName("错误选项3")]
        [PropertyOrder(4)]
        public string QuestionInfoWrongAnswerList2 { get; set; }

        [DisplayName("错误选项4")]
        [PropertyOrder(5)]
        public string QuestionInfoWrongAnswerList3 { get; set; }

        [DisplayName("错误选项5")]
        [PropertyOrder(6)]
        public string QuestionInfoWrongAnswerList4 { get; set; }

        [DisplayName("对话NPC")]
        [PropertyOrder(7)]
        public int QuestionInfoConversion { get; set; }
    }
}