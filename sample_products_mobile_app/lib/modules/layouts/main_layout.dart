import 'package:flutter/material.dart';

abstract class MainLayout extends StatelessWidget {
  MainLayout(this.appBar, this.content, this.footer,
      {this.contentSize = 10, this.footerSize = 2, this.footerActive = true})
      : super();

  final int contentSize;

  final int footerSize;

  final Widget appBar;

  final Widget content;

  final Widget footer;

  final bool footerActive;

  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      onWillPop: () async {
        return _onWilProp(context);
      },
      child: Container(
        padding: EdgeInsets.symmetric(vertical: 15, horizontal: 20),
        decoration: BoxDecoration(
            //TODO: set background
            // image: DecorationImage(

            //   image: imagesConstants.backgroundImage,
            //   fit: BoxFit.fill,
            // ),
            ),
        child: SafeArea(
          top: true,
          child: _body(context),
        ),
      ),
    );
  }

  Future<bool> _onWilProp(BuildContext context) async {
    return true;
  }

  Widget _body(BuildContext context) {
    // final decorationConstants = Provider.of<DecorationConstants>(context);
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget>[
        Expanded(
          flex: 2,
          child: appBar,
        ),
        Expanded(
          child: content,
          flex: this.contentSize,
        ),
        Visibility(
          visible: footerActive,
          child: Expanded(
            child: Container(
              // decoration: decorationConstants.bodyBg,
              child: footer,
            ),
            flex: this.footerSize,
          ),
        ),
      ],
    );
  }
}
