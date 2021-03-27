import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class AppBarStLess extends StatelessWidget {
  final String title;
  AppBarStLess(this.title);
  @override
  Widget build(BuildContext context) {
    return Container(
        // color: Colors.white,
        padding: EdgeInsets.symmetric(horizontal: 10, vertical: 2),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Flexible(
              fit: FlexFit.loose,
              child: Text(
                title,
                style: Theme.of(context).textTheme.headline6?.copyWith(
                    fontFamily: GoogleFonts.robotoCondensed().fontFamily,
                    fontWeight: FontWeight.w500),
              ),
            ),
            Flexible(
                child: Container(
              width: 50,
              height: 50,
              child: Stack(
                children: [
                  Positioned.fill(
                    child: Icon(
                      Icons.shopping_basket_outlined,
                      color: Colors.black,
                      size: Theme.of(context).textTheme.headline4?.fontSize,
                    ),
                    right: 0,
                    top: 0,
                  ),
                  Positioned(
                    right: 6,
                    top: 6,
                    child: Container(
                      width: (Theme.of(context).textTheme.headline4?.fontSize ??
                              25) *
                          .4,
                      height:
                          (Theme.of(context).textTheme.headline4?.fontSize ??
                                  25) *
                              .4,
                      decoration: BoxDecoration(
                          shape: BoxShape.circle, color: Colors.red[400]),
                      child: Text(
                        "0",
                        style: Theme.of(context)
                            .textTheme
                            .button
                            ?.copyWith(color: Colors.white, fontSize: 11),
                        textAlign: TextAlign.center,
                      ),
                    ),
                  ),
                ],
              ),
            ))
          ],
        ));
  }
}
