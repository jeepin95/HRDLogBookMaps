' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409


Imports Windows.Devices.Geolocation
Imports Windows.UI
Imports Windows.UI.Xaml.Controls.Maps
''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page
    Public r As ObservableCollection(Of HRDLogGrids.GridReached)
    Public polygons As List(Of MapPolygon)

    Private Async Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        Dim client As HRDLogGrids.Service1Client = New HRDLogGrids.Service1Client



        r = Await client.getGridsReachedAsync()


        For Each grid In r

            Dim wkt As String = grid.Shape.Geometry.WellKnownText.Remove(0, 10)
            wkt = wkt.Remove(wkt.Length - 2, 2)

            Dim arr As Array = wkt.Split(",")

            Dim polygon As New MapPolygon() With
            {
              .StrokeThickness = 5
            }
            Dim bgList As List(Of BasicGeoposition) = New List(Of BasicGeoposition)
            For Each p As String In arr
                Dim t As Array = p.Trim().Split(" ")
                Dim bg As New BasicGeoposition
                bg.Longitude = CDbl(t(0))

                bg.Latitude = CDbl(t(1))
                bgList.Add(bg)
            Next
            Dim gp As Geopath = New Geopath(bgList)

            polygon.Path = gp
            polygon.FillColor = Colors.Transparent
            polygon.StrokeColor = Colors.Red
            polygon.StrokeThickness = 1
            polygon.StrokeDashed = False

            Debug.WriteLine(polygon.ToString)
            myMap.MapElements.Add(polygon)





        Next




        '        var client = New ConnectODataEntities(New Uri("http://...ODATA URL..."));
        'var dsQuery = (DataServiceQuery < YOUR_METHOD_RETURN_TYPE >)(client.YOUR_METHOD);

        'var tf = New TaskFactory < IEnumerable < YOUR_METHOD_RETURN_TYPE >> ();
        'var List = (await tf.FromAsync(dsQuery.BeginExecute(null, null),
        '                            iar => dsQuery.EndExecute(iar))).ToList();


    End Sub
End Class
