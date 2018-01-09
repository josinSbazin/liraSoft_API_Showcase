using FEModel;
using FEModel.Element;
using FEModel.Interfaces;
using PureAbstractions;

namespace LiraSoftApiShowcase.Model
{
    public static class ModelCreator
    {
        /// <summary>
        ///  Создает объект новой модели Lira Soft
        /// </summary>
        /// <returns></returns>
        public static IModel CreateMainModel()
        {
            var model = new CMainModel();
            model.get_XdoModel().Active = false;
            return model;
        }

        /// <summary>
        ///  Создает пустую модель в Lira Soft
        /// </summary>
        public static void CreateEmptyModel(string filePath)
        {
            var model = CreateMainModel();
            LiraUtils.CreateModelFile(model, filePath);
        }

        /// <summary>
        ///  Создает простой стержень
        /// </summary>
        public static void CreateSimpleRod(string filePath)
        {
            var model = CreateMainModel();

            var point1 = new XYZ(0,0,0);
            var point2 = new XYZ(0,10,0);

            var archRod = new ArchitectureRod(model);

            KeyExternalProgram_Revit keyExternalProgram
                = (KeyExternalProgram_Revit)KeyExternalProgram.createKeyExternalProgram(e_Key_External_Program.KEP_REVIT);
            keyExternalProgram.setIntKey(123L);
            keyExternalProgram.setStringKey("test_rod");
            keyExternalProgram.setIntNumber(0);

            archRod.setKeyExternalProgram(keyExternalProgram);

            var polygon = new ArchitecturePolygon(archRod);
            var polygonalEdgeLine1 = CreatePolygonEdgeLine(polygon, point1);
            var polygonalEdgeLine2 = CreatePolygonEdgeLine(polygon, point2);

            polygon.addEdge(polygonalEdgeLine1);
            polygon.addEdge(polygonalEdgeLine2);

            archRod.setPolygon(polygon);
            model.addArchitectureElement(archRod);

            LiraUtils.CreateModelFile(model, filePath);
        }

        /// <summary>
        ///  Создает простую пластину
        /// </summary>
        public static void CreateSimplePlate(string filePath)
        {
            var model = CreateMainModel();

            var point1 = new XYZ(0,0,0);
            var point2 = new XYZ(0,10,0);
            var point3 = new XYZ(10,10,0);
            var point4 = new XYZ(10,0,0);

            var archPlate = new ArchitecturePlate(model);

            KeyExternalProgram_Revit keyExternalProgram
                = (KeyExternalProgram_Revit)KeyExternalProgram.createKeyExternalProgram(e_Key_External_Program.KEP_REVIT);
            keyExternalProgram.setIntKey(123L);
            keyExternalProgram.setStringKey("test_rod");
            keyExternalProgram.setIntNumber(0);

            archPlate.setKeyExternalProgram(keyExternalProgram);

            var polygon = new ArchitecturePolygon(archPlate);
            var polygonalEdgeLine1 = CreatePolygonEdgeLine(polygon, point1);
            var polygonalEdgeLine2 = CreatePolygonEdgeLine(polygon, point2);
            var polygonalEdgeLine3 = CreatePolygonEdgeLine(polygon, point3);
            var polygonalEdgeLine4 = CreatePolygonEdgeLine(polygon, point4);

            polygon.addEdge(polygonalEdgeLine1);
            polygon.addEdge(polygonalEdgeLine2);
            polygon.addEdge(polygonalEdgeLine3);
            polygon.addEdge(polygonalEdgeLine4);

            archPlate.addContour(polygon);
            model.addArchitectureElement(archPlate);

            LiraUtils.CreateModelFile(model, filePath);
        }

        private static PolygonEdgeLine CreatePolygonEdgeLine(ArchitecturePolygon parentPoly, XYZ point)
        {
            PolygonEdgeLine polygonEdgeLine = new PolygonEdgeLine(parentPoly);
            polygonEdgeLine.setXYZ(point.X, point.Y, point.Z);
            return polygonEdgeLine;
        }
    }
}