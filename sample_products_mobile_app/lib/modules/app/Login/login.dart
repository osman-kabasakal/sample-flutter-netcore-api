import 'dart:async';

import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:rxdart/subjects.dart';
import 'package:sample_products_mobile_app/constants/route_names.dart';
import 'package:sample_products_mobile_app/core/domain/entities/user.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';
import 'package:sample_products_mobile_app/utils/services/user_service.dart';
import 'package:url_launcher/url_launcher.dart';

class Login extends StatefulWidget {
  @override
  _LoginState createState() => _LoginState();
}

class _LoginState extends State<Login> {
  late UserService userService;

  late BehaviorSubject<User?> userSubject;

  StreamSubscription<User?>? userSubscribe;

  late bool isRequireSign;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    this.userService = context.getRequireProviderService<UserService>();
    this.userSubject = context.getRequireReactiveValue<User>()?.subject
        as BehaviorSubject<User?>;
    isRequireSign = userSubject.valueWrapper?.value == null;
    if (userSubscribe == null)
      this.userSubscribe = userSubject.listen((value) {
        setState(() {
          this.isRequireSign = value == null;
        });
      });
  }

  @override
  void didUpdateWidget(covariant Login oldWidget) {
    super.didUpdateWidget(oldWidget);
  }

  @override
  void dispose() {
    super.dispose();
    userSubscribe?.cancel();
  }

  final emailController = TextEditingController(text: "test@sample.com");
  final passwordController = TextEditingController(text: "Abc123+");

  @override
  Widget build(BuildContext context) {
    return Material(
        child: Visibility(
      visible: isRequireSign,
      child: Container(
        width: double.infinity,
        height: double.infinity,
        // color: Color(0xffECF1FA),
        decoration: BoxDecoration(
            image: DecorationImage(
          image: AssetImage("assets/images/1x/login_bg.png"),
          fit: BoxFit.fitWidth,
          alignment: Alignment.topLeft,
        )),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.end,
          children: [
            Flexible(
              flex: 5,
              child: Container(
                padding: EdgeInsets.only(top: 200),
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    // Flexible(
                    //     flex: 3,
                    //     child: Text(
                    //       "Seni Görmek Güzel",
                    //       style: Theme.of(context)
                    //           .textTheme
                    //           .headline3
                    //           ?.copyWith(color: Colors.orange),
                    //     ),
                    //     ),
                    Flexible(
                      fit: FlexFit.loose,
                      flex: 1,
                      child: Text(
                        "Hoşgeldin",
                        style: Theme.of(context).textTheme.headline2?.copyWith(
                              color: Colors.orange,
                              fontFamily: GoogleFonts.akronim().fontFamily,
                            ),
                      ),
                    ),
                    Flexible(
                      flex: 1,
                      fit: FlexFit.loose,
                      child: Container(
                        padding: EdgeInsets.all(0),
                        margin: EdgeInsets.symmetric(horizontal: 50),
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            TextFormField(
                              decoration: InputDecoration(),
                              controller: emailController,
                            ),
                            SizedBox(
                              height: 10,
                            ),
                            TextFormField(
                              decoration: InputDecoration(),
                              controller: emailController,
                              obscureText: true,
                              obscuringCharacter: "*",
                            ),
                          ],
                        ),
                      ),
                    ),
                    Flexible(
                      flex: 1,
                      fit: FlexFit.tight,
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: [
                          Flexible(
                            child: Container(
                              width: 300,
                              height: 50,
                              decoration: BoxDecoration(
                                borderRadius: BorderRadius.all(
                                  Radius.circular(200),
                                ),
                                color: Color(0xff2A2AC0),
                                boxShadow: [
                                  BoxShadow(
                                      offset: Offset.fromDirection(9, .5),
                                      blurRadius: 4,
                                      spreadRadius: .3)
                                ],
                              ),
                              clipBehavior: Clip.antiAlias,
                              child: TextButton(
                                onPressed: () {
                                  userService.signIn(emailController.text,
                                      passwordController.text);
                                },
                                child: Text(
                                  "Devam",
                                  style: Theme.of(context)
                                      .textTheme
                                      .headline3
                                      ?.copyWith(
                                          color: Color(0xffFFFFFF),
                                          fontSize: 16),
                                  textAlign: TextAlign.center,
                                ),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
            ),
            Flexible(
              flex: 1,
              child: Container(
                width: double.infinity,
                height: double.infinity,
                padding: EdgeInsets.only(top: 10),
                child: Center(
                  child: RichText(
                    text: TextSpan(
                      children: [
                        TextSpan(
                          text: "Örnek uygulamayı incelemek için ",
                          style: Theme.of(context).textTheme.bodyText1,
                        ),
                        TextSpan(
                          text: 'burayı ziyaret edebilirisiniz.',
                          style: new TextStyle(color: Color(0xff2A2AC0)),
                          recognizer: TapGestureRecognizer()
                            ..onTap = () async {
                              var sourceCodeUrl =
                                  "https://github.com/ossea/sample-flutter-netcore-api";
                              if (await canLaunch(sourceCodeUrl)) {
                                await launch(
                                  sourceCodeUrl,
                                  forceSafariVC: false,
                                  forceWebView: false,
                                );
                              }
                            },
                        ),
                      ],
                    ),
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    ));
  }
}
