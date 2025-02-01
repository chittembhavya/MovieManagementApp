using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieManagement.BLL;
using MovieManagement.Models;

namespace MovieManagementApp
{
    public partial class Form1 : Form
    {
        private readonly MovieService _movieService;
        private const string FilePath = @"C:\Practice\Assignment-28thjan\movies.json";

        public Form1()
        {
            InitializeComponent();
            _movieService = new MovieService();
            EnsureDirectoryExists();
        }


        private static void EnsureDirectoryExists()
        {
            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text) ||
                    string.IsNullOrWhiteSpace(txtTitle.Text) ||
                    string.IsNullOrWhiteSpace(txtGenre.Text) ||
                    string.IsNullOrWhiteSpace(txtYear.Text) ||
                    string.IsNullOrWhiteSpace(txtDirector.Text))
                {
                    MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _movieService.AddMovie(
                    txtId.Text,
                    txtTitle.Text,
                    txtGenre.Text,
                    int.Parse(txtYear.Text),
                    txtDirector.Text
                );
                MessageBox.Show("Movie added successfully.");
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Add Movie Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Please enter the Movie ID to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _movieService.DeleteMovie(txtId.Text);
                MessageBox.Show("Movie deleted successfully.");
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Delete Movie Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _movieService.SaveMovies(FilePath);
                MessageBox.Show("Movies saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Save Movie Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                _movieService.LoadMovies(FilePath);
                MessageBox.Show("Movies loaded successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Load Movie Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewMovies_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable movies = _movieService.GetMovies();
                listBoxMovies.Items.Clear();

                if (movies.Count == 0)
                {
                    MessageBox.Show("No movies to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DictionaryEntry entry in movies)
                {
                    if (entry.Value is Movie movie)
                    {
                        listBoxMovies.Items.Add(movie.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing movies: {ex.Message}", "View Movies Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtId.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtGenre.Text = string.Empty;
            txtYear.Text = string.Empty;
            txtDirector.Text = string.Empty;
        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }
    }
}
