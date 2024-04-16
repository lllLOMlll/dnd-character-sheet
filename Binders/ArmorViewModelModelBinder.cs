using CharacterSheetDnD.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace CharacterSheetDnD.Binders
{
	public class ArmorViewModelModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			var armorTypeValue = bindingContext.ValueProvider.GetValue("ArmorType").FirstValue;

			ArmorViewModel? model = armorTypeValue switch
			{
				"Light" => new LightArmorViewModel(),
				"Medium" => new MediumArmorViewModel(),
				"Heavy" => new HeavyArmorViewModel(),
				"Shield" => new ShieldViewModel(),
				_ => null
			};

			if (model == null)
			{
				return Task.CompletedTask;
			}

			bindingContext.Result = ModelBindingResult.Success(model);
			return Task.CompletedTask;
		}
	}

}
