using RucheHome.Voiceroid;
using System;
using System.Threading.Tasks;

namespace KissServer
{
    class VProcess
    {
        private readonly IProcess process;

        public string voiceroidName { get; set; }
        public string Name { get; set; }
        public string Product { get; set; }
        public string DisplayProduct { get; set; }
        public bool HasMultiCharacters { get; set; }
        public bool CanSaveBlankText { get; set; }
        public bool IsStartup { get; set; }
        public bool IsRunning { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsSaving { get; set; }
        public bool DialogShowing { get; set; }
        public string TalkText { get; set; }
        public string CharacterName { get; set; }

        public VProcess(IProcess process)
        {
            this.process = process;
            voiceroidName = Enum.GetName(typeof(VoiceroidId), process.Id);
            Name = process.Name;
            Product = process.Product;
            DisplayProduct = process.DisplayProduct;
            HasMultiCharacters = process.HasMultiCharacters;
            CanSaveBlankText = process.CanSaveBlankText;
            IsStartup = process.IsStartup;
            IsRunning = process.IsRunning;
            IsPlaying = process.IsPlaying;
            IsSaving = process.IsSaving;
            DialogShowing = process.IsDialogShowing;
        }

        public async Task Update()
        {
            TalkText = await process.GetTalkText();
            CharacterName = await process.GetCharacterName();
        }
    }
}
