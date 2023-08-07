﻿using System;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        private User m_LoggedInUser;
        private List<IViewer> viewers;

        public FormMain(User i_LoggedInUser)
        {
            m_LoggedInUser = i_LoggedInUser;
            viewers = new List<IViewer>();
            InitializeComponent();
            FacebookService.s_CollectionLimit = 25;
            initProfileInformation();
            viewers.Add(new AlbumViewer());
            viewers.Add(new GroupViewer());
        }

        private void initProfileInformation()
        {
            buttonLogout.Enabled = true;
            labelName.Text = m_LoggedInUser.Name;
            pictureBoxProfile.ImageLocation = m_LoggedInUser.PictureNormalURL;
            labelBirthDay.Text = m_LoggedInUser.Birthday;

            DateTime userBirthDay = DateTime.ParseExact(
                m_LoggedInUser.Birthday,
                "MM/dd/yyyy",
                System.Globalization.CultureInfo.InvariantCulture);

            labelAge.Text = CalculateAge(userBirthDay);
            labelEmail.Text = m_LoggedInUser.Email;
            labelCity.Text = m_LoggedInUser.Location.Name;
            labelGender.Text = m_LoggedInUser.Gender.ToString();
        }

        private string CalculateAge(DateTime i_BirthDay)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - i_BirthDay.Year;

            if (i_BirthDay > today.AddYears(-age))
            {
                age--;
            }

            return age.ToString();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.LogoutWithUI();
            m_LoggedInUser = null;
            this.Close();
        }

        private void buttonPosts_Click(object sender, EventArgs e)
        {
            switchShownContent("Posts");
            fetchPosts();
        }

        private void buttonAlbums_Click(object sender, EventArgs e)
        {
            switchShownContent("Albums");
            listBoxContent.DisplayMember = "Name";
            fetchAlbums();
        }

        private void buttonGroups_Click(object sender, EventArgs e)
        {
            switchShownContent("Groups");
            listBoxContent.DisplayMember = "Name";
            fetchGroups();
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            switchShownContent("Events");
            listBoxContent.DisplayMember = "Name";
            fetchEvents();
        }

        private void buttonFavoriteTeams_Click(object sender, EventArgs e)
        {
            switchShownContent("Favofrite Teams");
            listBoxContent.DisplayMember = "Name";
            fetchFavoriteTeams();
        }

        private void buttonLikedPages_Click(object sender, EventArgs e)
        {
            switchShownContent("Liked Pages");
            listBoxContent.DisplayMember = "Name";
            fetchLikedPages();
        }

        private void buttonFriends_Click(object sender, EventArgs e)
        {
            switchShownContent("Friends");
            listBoxContent.DisplayMember = "Name";

            fetchFriends();
        }

        private void fetchFriends()
        {
            try
            {
                foreach (User friend in m_LoggedInUser.Friends)
                {
                    listBoxContent.Items.Add(friend);
                }

                if (listBoxContent.Items.Count == 0)
                {
                    throw new NoDataAvailableException();
                }

            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Constants.NO_ITEMS_TO_RETREIVE_MESSAGE, "friends"));
            }
        }

        private void fetchPosts()
        {
            try
            {
                foreach (Post post in m_LoggedInUser.Posts)
                {
                    /*if (post.Message != null)*/
                    {
                        listBoxContent.Items.Add(post);
                    }
                    /*else if (post.Caption != null)
                    {
                        listBoxContent.Items.Add(post);
                    }
                    else
                    {
                        listBoxContent.Items.Add(string.Format("[{0}]", post.));
                    }*/
                }

                if (listBoxContent.Items.Count == 0)
                {
                    throw new NoDataAvailableException();
                }
            }
            catch (NoDataAvailableException noDataAvailableException)
            {
                MessageBox.Show(noDataAvailableException.Message);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Constants.NO_ITEMS_TO_RETREIVE_MESSAGE, "Posts"));
            }
        }

        private void fetchAlbums()
        {
            try
            {
                foreach (Album album in m_LoggedInUser.Albums)
                {
                    listBoxContent.Items.Add(album);
                }

                if (listBoxContent.Items.Count == 0)
                {
                    throw new NoDataAvailableException();
                }
            }
            catch (NoDataAvailableException noDataAvailableException)
            {
                MessageBox.Show(noDataAvailableException.Message);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Constants.NO_ITEMS_TO_RETREIVE_MESSAGE, "Albums"));
            }
        }

        private void fetchGroups()
        {
            try
            {
                foreach (Group group in m_LoggedInUser.Groups)
                {
                    listBoxContent.Items.Add(group);
                }

                if (listBoxContent.Items.Count == 0)
                {
                    throw new NoDataAvailableException();
                }
            }
            catch (NoDataAvailableException noDataAvailableException)
            {
                MessageBox.Show(noDataAvailableException.Message);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Constants.NO_ITEMS_TO_RETREIVE_MESSAGE, "groups"));
            }
        }

        private void fetchEvents()
        {
            try
            {
                foreach (Event fbEvent in m_LoggedInUser.Events)
                {
                    listBoxContent.Items.Add(fbEvent.Name);
                }

                if (listBoxContent.Items.Count == 0)
                {
                    throw new NoDataAvailableException();
                }
            }
            catch (NoDataAvailableException noDataAvailableException)
            {
                MessageBox.Show(noDataAvailableException.Message);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Constants.NO_ITEMS_TO_RETREIVE_MESSAGE, "Events"));
            }
        }

        private void fetchFavoriteTeams()
        {
            try
            {
                foreach (Page team in m_LoggedInUser.FavofriteTeams)
                {
                    listBoxContent.Items.Add(team.Name);
                }

                if (listBoxContent.Items.Count == 0)
                {
                    throw new NoDataAvailableException();
                }
            }
            catch (NoDataAvailableException noDataAvailableException)
            {
                MessageBox.Show(noDataAvailableException.Message);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Constants.NO_ITEMS_TO_RETREIVE_MESSAGE, "Favorite Teams"));
            }
        }

        private void fetchLikedPages()
        {
            try
            {
                foreach (Page page in m_LoggedInUser.LikedPages)
                {
                    listBoxContent.Items.Add(page);
                }

                if (listBoxContent.Items.Count == 0)
                {
                    throw new NoDataAvailableException();
                }
            }
            catch (NoDataAvailableException noDataAvailableException)
            {
                MessageBox.Show(noDataAvailableException.Message);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Constants.NO_ITEMS_TO_RETREIVE_MESSAGE, "liked pages"));
            }
        }

        private void switchShownContent(string i_ContentCategoryName)
        {
            labelViewTitle.Text = string.Format("{0}:", i_ContentCategoryName);
            listBoxContent.Items.Clear();
            pictureBoxSelectedContent.Image = null;
            hideAllViewers();
        }

        private void hideAllViewers()
        {
            foreach (IViewer viewer in viewers)
            {
                viewer.HideControls();
            }
        }

        private void listBoxContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selectedItem = listBoxContent.SelectedItem;

            switch (selectedItem)
            {
                case Post selectedPost:
                    loadPostDetails(selectedPost);
                    break;

                case User selectedFriend:
                    pictureBoxSelectedContent.LoadAsync(selectedFriend.PictureNormalURL);
                    break;

                case Album selectedAlbum:
                    fetchAlbumPictures(selectedAlbum);
                    break;

                case Group selectedGroup:
                    loadGroupDetails(selectedGroup);

                    break;

                case Event selectedEvent:
                    pictureBoxSelectedContent.LoadAsync(selectedEvent.PictureNormalURL);
                    break;

                case Page selectedPage:
                    pictureBoxSelectedContent.LoadAsync(selectedPage.PictureNormalURL);
                    break;
            }
        }

        private void loadPostDetails(Post selectedPost)
        {

        }

        private void loadGroupDetails(Group i_SelectedGroup)
        {
            pictureBoxSelectedContent.LoadAsync(i_SelectedGroup.PictureNormalURL);
            (viewers[(int)eViewerIndex.GroupViewerIndex] as GroupViewer).LoadGroupDetailsToComponents(i_SelectedGroup);
        }

        private void fetchAlbumPictures(Album i_SelectedAlbum)
        {
            pictureBoxSelectedContent.LoadAsync(i_SelectedAlbum.PictureAlbumURL);
            (viewers[(int)eViewerIndex.AlbumViewerIndex] as AlbumViewer).LoadPicturesToListBox(i_SelectedAlbum);
        }
    }
}
