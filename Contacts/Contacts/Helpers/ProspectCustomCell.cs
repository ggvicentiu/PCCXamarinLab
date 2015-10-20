using System;
using System.Collections.Generic;
using Contacts.Helpers;
using Contacts.Models;
using Contacts.ViewModels;
using Contacts.Views;
using Xamarin.Forms;

namespace Contacts
{
    public class ProspectCustomCell : ViewCell
    {
        private readonly View _infoButtonImage = new Image { Source = "info.png" };
        private readonly View _plusButtonImage = new Image { Source = "plus.png" };
        private readonly View _minusButtonImage = new Image { Source = "minus.png" };
        private const string ImageOn = "bullet_light_icon_100x100.png";
        private const string ImageOff = "bullet_dark_icon_100x100.png";

        public ProspectCustomCell(Label label, int position, int positionsCount, VisualElement parent, Prospect prospect)
        {
            label.LineBreakMode = LineBreakMode.WordWrap;
            label.TextColor = Color.White;
            AddGestures(label, parent, prospect);

            var buttonsRow = new Grid
            {
                ColumnDefinitions =
                {

                    new ColumnDefinition {Width = new GridLength(25, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(25, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(25, GridUnitType.Absolute)},
                },
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Padding = 5,
                Children = { { _infoButtonImage, 0, 0 }, { _plusButtonImage, 1, 0 }, { _minusButtonImage, 2, 0 } }
            };

            var row = new Grid
            {

                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(60, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(38, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},

                },

                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Children = { { label, 1, 0 }, { buttonsRow, 2, 0 } }
            };

            var positions = new Grid
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                ColumnSpacing = 5,
            };
            for (int i = 0; i < positionsCount; i++)
            {
                positions.Children.Add(new Image { Source = i == position ? ImageOn : ImageOff }, i, 0);
                positions.ColumnDefinitions.Add(new ColumnDefinition { Width = 10 });
            }



            var content = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition{Height = new GridLength(80, GridUnitType.Star) },
                    new RowDefinition{Height = new GridLength(20, GridUnitType.Star)}
                },
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Children = { { row, 0, 0 }, { positions, 0, 1 } },
                BackgroundColor = Color.Teal


            };

            this.View = content;
            this.View.VerticalOptions = LayoutOptions.CenterAndExpand;
        }

        static Contact ConvertPropectToContact(Prospect prospect)
        {
            var contact = new Contact
            {
                Address = prospect.Location,
                Name = prospect.Name,
                Notes = prospect.Notes,
                Phone = prospect.Phone,
                PicUrl = prospect.PicUrl
            };

            return contact;
        }

        private void AddGestures(Label ttd, VisualElement parent, Prospect prospect)
        {
            var tapInfoButton = new TapGestureRecognizer();
            tapInfoButton.Tapped += (s, e) =>
            parent.Navigation.PushModalAsync(new ProspectDetailView
            {
                PDvm = new ProspectDetailViewModel(prospect)
            }, false);
            ttd.GestureRecognizers.Add(tapInfoButton);
            _infoButtonImage.GestureRecognizers.Add(tapInfoButton);

            var tapPlus = new TapGestureRecognizer();
            tapPlus.Tapped += async (s, e) =>
            {
                var p = ConvertPropectToContact(prospect);
                await ProspectDetailViewModel.AddContact(p);
                await API.UpdateStatus(prospect, API.Status.Converted);
                await parent.Navigation.PushModalAsync(new MainView());
            };
            _plusButtonImage.GestureRecognizers.Add(tapPlus);

            var tapMinus = new TapGestureRecognizer();
            tapMinus.Tapped += async (s, e) =>
            {
                var contact = ConvertPropectToContact(prospect);
                await API.UpdateStatus (prospect, API.Status.Ignored);
                await parent.Navigation.PushModalAsync(new MainView());
            };
            _minusButtonImage.GestureRecognizers.Add(tapMinus);
        }


    }
}

