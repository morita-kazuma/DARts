using System;

namespace Coordinate
{
    public static class cXY
    {
        //**********************************************************************************
        //   UTM座標及びゾーンから緯度経度を取得
        //   (北緯の東経地域に限る。原点緯度は北緯0度。数式は以下を参照。)
        // https://vldb.gsi.go.jp/sokuchi/surveycalc/surveycalc/algorithm/xy2bl/xy2bl.htm
        //**********************************************************************************

        public static void UTMToDes(double E, double N, double Zone, out double latitude, out double longitude)
        {
            double f, r, n, Am, n1, n2, n3, n4, n5, n6;
            double[] A = new double[6];
            double lon0, lat, lat0, gd, ed, E0, N0, k0, g, e, kai, rk0, Smlat;
            double[] b = new double[6];
            double[] d = new double[7];
            int i;

            //	f = 1.0/ (298.257222101);//GRS80 地球の扁平率
            f = 1.0d / 298.257223563d;//WGS84 地球の扁平率
            r = 6378137.0d; //GRS80 & WGS84 地球長辺半径

            E0 = 500.0d * 1000.0d;// 経度オフセット(500km)
            N0 = 0.0d;// 北半球オフセット
                     //	N0 = 10000.0*1000.0;// 南半球オフセット(10000km)
            k0 = 0.9996d;//UTM座標系の場合
                        //	k0 = 0.9999;//国土地理院

            n = f / (2.0d - f);
            n1 = n;
            n2 = n1 * n;
            n3 = n2 * n;
            n4 = n3 * n;
            n5 = n4 * n;
            n6 = n5 * n;
            A[0] = 1.0d + n2 / 4.0d + n4 / 64.0d;
            rk0 = r * k0 / (1.0d + n1);
            Am = rk0 * A[0];

            A[1] = -3.0d / 2.0d * (n1 - n3 / 8.0d - n5 / 64.0d);
            A[2] = 15.0d / 16.0d * (n2 - n4 / 4.0d);
            A[3] = -35.0d / 48.0d * (n3 - 5.0d / 16.0d * n5);
            A[4] = 315.0d / 512.0d * n4;
            A[5] = -693.0d / 1280.0d * n5;

            b[1] = 1.0d / 2.0d * n1 - 2.0d / 3.0d * n2 + 37.0d / 96.0d * n3 - 1.0d / 360.0d * n4 - 81.0d / 512.0d * n5;
            b[2] = 1.0d / 48.0d * n2 + 1.0d / 15.0d * n3 - 437.0d / 1440.0d * n4 + 46.0d / 105.0d * n5;
            b[3] = 17.0d / 480.0d * n3 - 37.0d / 840.0d * n4 - 209.0d / 4480.0d * n5;
            b[4] = 4397.0d / 161280.0d * n4 - 11.0d / 504.0d * n5;
            b[5] = 4583.0d / 161280.0d * n5;

            d[1] = 2.0 * n - 2.0d / 3.0d * n2 - 2.0 * n3 + 116.0d / 45.0d * n4 + 26.0d / 45.0d * n5 - 2854.0d / 675.0d * n6;
            d[2] = 7.0d / 3.0d * n2 - 8.0d / 5.0d * n3 - 227.0d / 45.0d * n4 + 2704.0d / 315.0d * n5 + 2323.0d / 945.0d * n6;
            d[3] = 56.0d / 15.0d * n3 - 136.0d / 35.0d * n4 - 1262.0d / 105.0d * n5 + 73814.0d / 2835.0d * n6;
            d[4] = 4279.0d / 630.0d * n4 - 332.0d / 35.0d * n5 - 399572.0d / 14175.0d * n6;
            d[5] = 4174.0d / 315.0d * n5 - 144838.0d / 6237.0d * n6;
            d[6] = 601676.0d / 22275.0d * n6;

            lon0 = ((Zone - 31.0d) * 6.0d) + 3.0d;
            lat0 = 0.0;//赤道固定

            lat0 = lat0 * Math.PI;

            //緯度原点の調整分の計算
            Smlat = Am * lat0;
            for (i = 1; i <= 5; i++)
            {
                Smlat = Smlat + rk0 * A[i] * Math.Sin(2.0d * ((double)i) * lat0);
            }

            g = (N - N0 + Smlat) / Am;
            e = (E - E0) / Am;

            gd = g;
            ed = e;

            for (i = 1; i <= 5; i++)
            {
                gd = gd - b[i] * Math.Sin(2.0d * ((double)i) * g) * Math.Cosh(2.0d * ((double)i) * e);
                ed = ed - b[i] * Math.Cos(2.0d * ((double)i) * g) * Math.Sinh(2.0d * ((double)i) * e);
            }

            kai = Math.Asin(Math.Sin(gd) / Math.Cosh(ed));
            lat = kai;
            for (i = 1; i <= 6; i++)
            {
                lat = lat + d[i] * Math.Sin(2.0d * ((double)i) * kai);
            }

            latitude = lat * 180.0d/ Math.PI;
            longitude = lon0 + Math.Atan(Math.Sinh(ed) / Math.Cos(gd)) * 180.0d / Math.PI;

        }
    }
    
}